import { createApp } from 'vue';
import App from './App.vue';
import { createI18n } from 'vue-i18n';
import { routes } from './router.ts';
import { createPinia } from 'pinia';
import Toast from 'vue-toastification';
import { vClickOutside } from './directives/click-outside.directive.ts';
import ApiClient from './services/api.service.ts';
import LoadingService from './services/loading.service.ts';
import ThemeService from './services/theme.service.ts';
import messages from '@intlify/unplugin-vue-i18n/messages';
import '@fortawesome/fontawesome-free/js/all.js';
import AuthService from './services/auth.service.ts';

(async () => {
  const router = routes();
  const apiClient = new ApiClient(import.meta.env.VITE_URL_API);

  createApp(App)
    .use(
      createI18n({
        legacy: false,
        globalInjection: true,
        locale: localStorage.getItem('locale') ?? (window.navigator.language.includes('en') ? 'en' : 'fr'),
        fallbackLocale: 'en',
        availableLocales: ['en', 'fr'],
        messages,
      })
    )
    .use(router)
    .use(createPinia())
    .use(Toast, { timeout: 2000 })
    .directive('clickOutside', vClickOutside)
    .provide('apiClient', apiClient)
    .provide('loadingService', new LoadingService())
    .provide('themeService', new ThemeService())
    .provide('authService', new AuthService(apiClient.client, router))
    .mount('#app');
})();
