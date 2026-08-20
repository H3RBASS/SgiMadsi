const CACHE_NAME = "sgimadsi-v4.1";

// [FIX] Assets estáticos para precachear (offline real)
const PRECACHE_ASSETS = [
    "/SgiMadsi/",
    "/SgiMadsi/manifest.webmanifest",
    "/SgiMadsi/offline.html",
    "/SgiMadsi/css/tailwind.css",
    "/SgiMadsi/css/app.css",
    "/SgiMadsi/css/fonts/nunito-regular.woff2",
    "/SgiMadsi/css/fonts/nunito-semibold.woff2",
    "/SgiMadsi/css/fonts/nunito-bold.woff2",
    "/SgiMadsi/Icons/android-chrome-192x192.png",
    "/SgiMadsi/Icons/android-chrome-512x512.png",
    "/SgiMadsi/Icons/favicon.ico",
    "/SgiMadsi/Icons/apple-touch-icon.png"
];

self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => cache.addAll(PRECACHE_ASSETS))
            .then(() => self.skipWaiting())
    );
});

self.addEventListener("activate", event => {
    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(
                keys
                    .filter(key => key !== CACHE_NAME)
                    .map(key => caches.delete(key))
            )
        ).then(() => self.clients.claim())
    );
});

self.addEventListener("fetch", event => {
    if (event.request.method !== "GET") {
        return;
    }

    const url = new URL(event.request.url);

    // No interceptar peticiones externas
    if (url.origin !== self.location.origin) {
        return;
    }

    event.respondWith(
        caches.match(event.request)
            .then(cachedResponse => {
                if (cachedResponse) {
                    return cachedResponse;
                }

                return fetch(event.request)
                    .then(response => {
                        // No cachear respuestas no válidas ni de API
                        if (!response || response.status !== 200) {
                            return response;
                        }

                        // No cachear peticiones a la API de Supabase
                        if (url.pathname.includes("/rest/v1/") || url.pathname.includes("/auth/")) {
                            return response;
                        }

                        const responseClone = response.clone();

                        caches.open(CACHE_NAME)
                            .then(cache => {
                                cache.put(event.request, responseClone);
                            });

                        return response;
                    })
                    .catch(() => {
                        // [FIX] Si es navegación, mostrar offline.html
                        if (event.request.mode === "navigate") {
                            return caches.match("/SgiMadsi/offline.html");
                        }
                        return new Response("Offline", { status: 503 });
                    });
            })
    );
});