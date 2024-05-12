import vue from "@vitejs/plugin-vue";
import { defineConfig } from "vite";

//const backend = import.meta.env.VITE_APP_API_URL;

export default defineConfig(cfg =>
  {const backend = cfg.mode;
    return {
  plugins: [vue()],
  server: { host: "0.0.0.0",
    proxy: {
      "^/api": {
        target: backend === 'production' ? 'http://backend:80' : 'https://localhost:7107',
        ws: false,
        secure: false,
      },
      "^/hub": {
        target: backend === 'production' ? 'http://backend:80' : 'https://localhost:7107',
        ws: true,
        secure: false,
      },
    },
  },
}});
