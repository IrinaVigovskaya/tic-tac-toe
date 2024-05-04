import vue from "@vitejs/plugin-vue";
import { defineConfig } from "vite";

const backend = import.meta.env.VITE_APP_API_URL;

export default defineConfig({
  plugins: [vue()],
  server: { host: "0.0.0.0",
    proxy: {
      "^/api": {
        target: backend,
        ws: false,
        secure: false,
      },
      "^/hub": {
        target: backend,
        ws: true,
        secure: false,
      },
    },
  },
});

