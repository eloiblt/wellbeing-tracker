<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type LoadingService from './services/loading.service.ts';
import type ThemeService from './services/theme.service.ts';
import Loader from './components/shared/Loader.vue';
import Navbar from './components/shared/Navbar.vue';

const loadingService = inject<LoadingService>('loadingService') as LoadingService;
const themeService = inject<ThemeService>('themeService') as ThemeService;
const loading = ref<boolean>(false);
const theme = ref<string>('light');

onMounted(() => {
  loadingService.loading$.subscribe(value => (loading.value = value));
  themeService.theme$.subscribe(newTheme => (theme.value = newTheme));
});
</script>

<template>
  <div class="app-container" :class="theme">
    <Navbar></Navbar>
    <div class="app-scroll-container">
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

* {
  font-family: BellSlim, serif;
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