<script setup lang="ts">
import LanguageSelector from './LanguageSelector.vue';
import { inject, onMounted, ref } from 'vue';
import type Keycloak from 'keycloak-js/dist/keycloak.js';
import { useI18n } from 'vue-i18n';
import ToggleSwitch from './ToggleSwitch.vue';

const keycloak = inject<Keycloak>('keycloak') as Keycloak;
const { t } = useI18n();

const env = ref<string>(import.meta.env.VITE_ENVIRONMENT);
const isAdmin = ref<boolean>(false);

onMounted(() => {
  if (!keycloak?.resourceAccess) return;
  isAdmin.value = keycloak.resourceAccess.nrt?.roles.includes('NRT-Admin') ?? false;
});
</script>

<template>
  <nav class="navbar">
    <router-link to="/">
      <img alt="" class="logo" src="../../assets/clock.png" />
    </router-link>

    <router-link class="app-name-container" to="/">
      <h1 class="app-name-top">{{ t('app-title') }}</h1>
    </router-link>

    <!--    <div class="navbar-menu">-->
    <!--      <router-link class="menu-item" to="table">Table</router-link>-->
    <!--      <router-link class="menu-item" to="/dropdowns">Dropdowns et Modals</router-link>-->
    <!--    </div>-->

    <ul class="navbar-actions">
      <li class="nav-item">
        <ToggleSwitch></ToggleSwitch>
      </li>

      <li class="nav-item language-selector">
        <LanguageSelector />
      </li>
    </ul>
  </nav>
</template>

<style lang="scss" scoped>
.navbar {
  background-color: var(--background-navbar);
  display: flex;
  align-items: center;
  padding: 12px 30px;

  .logo {
    cursor: pointer;
    width: 50px;
  }

  .app-name-container {
    display: flex;
    flex-direction: column;

    h1 {
      margin-left: 20px;
      cursor: pointer;
      font-size: 18px;
      margin-right: 20px;
      color: var(--text-tertiary);
    }
  }

  .navbar-menu {
    display: flex;
    gap: 20px;

    .menu-item {
      font-weight: bold;
      color: var(--text-tertiary);

      &.router-link-active {
        text-decoration-thickness: 2px;
        text-decoration-line: underline;
        text-underline-offset: 5px;
      }
    }
  }

  .navbar-actions {
    display: flex;
    gap: 10px;
    align-items: center;
    margin-left: auto;

    .nav-item {
      cursor: pointer;
      color: var(--text-tertiary);

      > a {
        display: flex;
        align-items: center;
        text-decoration: none;
        color: var(--text-tertiary);

        p {
          margin-left: 5px;
        }
      }

      &.language-selector {
        position: relative;
        cursor: default;
      }
    }
  }
}
</style>
