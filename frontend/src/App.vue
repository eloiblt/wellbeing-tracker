<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type LoadingService from './services/loading.service.ts';
import type ThemeService from './services/theme.service.ts';
import Loader from './components/shared/Loader.vue';
import Navbar from './components/shared/Navbar.vue';
import Menu from './components/shared/Menu.vue';
import { useRouter } from 'vue-router';
import type AuthService from './services/auth.service.ts';

const authService = inject<AuthService>('authService') as AuthService;
const loadingService = inject<LoadingService>('loadingService') as LoadingService;
const themeService = inject<ThemeService>('themeService') as ThemeService;
const loading = ref<boolean>(false);
const theme = ref<string>('light');
const isMenuOpen = ref<boolean>(false);
const router = useRouter();

onMounted(() => {
  loadingService.loading$.subscribe(value => (loading.value = value));
  themeService.theme$.subscribe(newTheme => (theme.value = newTheme));

  router.afterEach(async () => {
    isMenuOpen.value = false;
  });

  router.beforeEach(async to => {
    if (!authService.logged && to.name !== 'Login') {
      return { name: 'Login' };
    }
  });
});
</script>

<template>
  <div class="app-container" :class="theme">
    <Navbar :menu-open="isMenuOpen" @toggle-menu="isMenuOpen = !isMenuOpen"></Navbar>
    <div class="app-scroll-container">
      <Menu :open="isMenuOpen"></Menu>
      <RouterView v-slot="{ Component, route }">
        <Transition name="fade" mode="out-in">
          <div :key="route.path" class="app-scroll">
            <component :is="Component" />
          </div>
        </Transition>
      </RouterView>
    </div>
  </div>

  <Loader v-if="loading"></Loader>
</template>

<style lang="scss">
@import 'vue-toastification/dist/index.css';
@import '@fortawesome/fontawesome-free/css/all.css';
@import './styles/reset';
@import './styles/theme.dark';
@import './styles/theme.light';
@import './styles/heading';
@import './styles/structure';
@import './styles/vue-select';
@import './styles/buttons';
@import '@fontsource/roboto';

* {
  font-family: Roboto, serif;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.1s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

::-webkit-scrollbar {
  width: 10px;
  height: 10px;
  cursor: pointer;
}

::-webkit-scrollbar-track {
  box-shadow: inset 0 0 4px lighten(grey, 20%);
  border-radius: 10px;
}

::-webkit-scrollbar-thumb {
  background: lighten(grey, 20%);
  border-radius: 10px;
}

::-webkit-scrollbar-thumb:hover {
  background: grey;
}
</style>
