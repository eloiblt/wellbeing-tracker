<script setup lang="ts">
import { useI18n } from 'vue-i18n';

const { locale } = useI18n({});
function changeLocale() {
  locale.value = locale.value === 'en' ? 'fr' : 'en';
  localStorage.setItem('locale', locale.value);
}
</script>

<template>
  <button class="btn-no-style" @click.prevent="changeLocale">
    {{ (locale === 'en' ? 'fr' : 'en').toUpperCase() }}
  </button>
</template>

<style scoped lang="scss">
button {
  cursor: pointer;
  font-size: 15px;
  color: var(--text-tertiary);
}
</style>
