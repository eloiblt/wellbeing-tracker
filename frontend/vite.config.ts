import vue from '@vitejs/plugin-vue';
import VueI18nPlugin from '@intlify/unplugin-vue-i18n/vite';
import { dirname, resolve } from 'path';
import { fileURLToPath } from 'url';

export default {
  plugins: [
    vue(),
    VueI18nPlugin({
      include: resolve(dirname(fileURLToPath(import.meta.url)), './src/locales/**'),
    }),
  ],
  envDir: './env',
  server: {
    port: 4000,
  },
  test: {
    include: ['tests/**/*.test.ts'],
    globals: true,
    css: true,
    environment: 'jsdom',
    setupFiles: 'tests/config/vitest.setup.ts',
    coverage: {
      provider: 'v8',
      reporter: ['text', 'json', 'html', 'cobertura'],
    },
  },
};
