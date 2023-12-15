<script setup lang="ts">
import { firstValueFrom } from 'rxjs';
import { inject, ref } from 'vue';
import type ApiClient from '../services/api.service.ts';
import type AuthService from '../services/auth.service.ts';

const authService = inject<AuthService>('authService') as AuthService;
const apiClient = inject<ApiClient>('apiClient') as ApiClient;

const email = ref<string>();
const password = ref<string>();

async function login() {
  const token = (await firstValueFrom(apiClient.login())).data;
  await authService.login(token);
}
</script>

<template>
  <form v-on:submit.prevent="login">
    <h1>Se connecter</h1>

    <div class="input-label">
      <label>Email</label>
      <input type="email" v-model="email" />
    </div>

    <div class="input-label">
      <label>Password</label>
      <input type="email" v-model="email" />
    </div>
  </form>
  <button type="button" @click="login">Login</button>
</template>

<style scoped lang="scss"></style>
