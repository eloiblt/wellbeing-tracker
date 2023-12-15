<script setup lang="ts">
import LanguageSelector from './LanguageSelector.vue';
import { useI18n } from 'vue-i18n';
import ToggleSwitch from './ToggleSwitch.vue';
import { inject, onMounted, ref } from 'vue';
import type AuthService from '../../services/auth.service.ts';

const { t } = useI18n();
const authService = inject<AuthService>('authService') as AuthService;
const isLoggedIn = ref<boolean>(false);
const emit = defineEmits(['toggle-menu']);

const props = defineProps({
  menuOpen: {
    type: Boolean,
    default: false,
  },
});

onMounted(() => {
  authService.loggedSubject.subscribe(v => (isLoggedIn.value = v));
});

function menuOpenToggle() {
  emit('toggle-menu');
}
</script>

<template>
  <nav class="navbar">
    <div class="menu-icon">
      <Transition name="fade">
        <button v-if="!props.menuOpen" type="button" class="btn-no-style" @click="menuOpenToggle">
          <i class="fa-solid fa-bars"></i>
        </button>
      </Transition>
      <Transition name="fade">
        <button v-if="props.menuOpen" type="button" class="btn-no-style" @click="menuOpenToggle">
          <i class="fa-solid fa-xmark"></i>
        </button>
      </Transition>
    </div>

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
        <ToggleSwitch />
      </li>

      <li class="nav-item">
        <LanguageSelector />
      </li>

      <li v-if="isLoggedIn" class="nav-item" title="'logout'" @click="authService.logout()">
        <i class="fa-solid fa-arrow-right-from-bracket"></i>
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
  caret-color: transparent;
  filter: drop-shadow(0px 5px 5px var(--drop-shadow));
  z-index: 1;

  .menu-icon {
    position: relative;
    padding-right: 20px;
    width: 30px;
    height: 30px;

    button {
      margin-right: 20px;
      font-size: 20px;
      color: var(--text-tertiary);
      position: absolute;
      inset: 0;
    }
  }

  .logo {
    cursor: pointer;
    width: 30px;
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
    }
  }
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
