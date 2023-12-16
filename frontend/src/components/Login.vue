<script setup lang="ts">
import { inject, ref } from 'vue';
import type ApiClient from '../services/api.service.ts';
import type AuthService from '../services/auth.service.ts';

const authService = inject<AuthService>('authService') as AuthService;
const apiClient = inject<ApiClient>('apiClient') as ApiClient;

const email = ref<string>();
const password = ref<string>();
const error = ref<boolean>(false);

async function login() {
  apiClient.login({ email: email.value!, password: password.value! }).subscribe({
    next: async res => {
      await authService.login(res.data);
      error.value = false;
    },
    error: () => {
      error.value = true;
    },
  });
}
</script>

<template>
  <div class="login-container">
    <form v-on:submit.prevent="login">
      <h1>Login</h1>

      <p class="error" v-if="error">Invalid email or password</p>

      <div class="input-label">
        <label>Email</label>
        <input type="email" v-model="email" />
      </div>

      <div class="input-label">
        <label>Password</label>
        <input type="password" v-model="password" />
      </div>

      <div class="form-actions">
        <router-link class="app-name-container" to="/signup">
          <button type="button" class="btn-no-style">Signup</button>
        </router-link>
        <button class="btn-primary" :disabled="!email?.trim() || !password?.trim()" type="submit">Login</button>
      </div>
    </form>
  </div>
</template>

<style scoped lang="scss">
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;

  form {
    display: flex;
    flex-direction: column;
    gap: 15px;
    width: 30%;

    .error {
      color: red;
    }

    h1 {
      align-self: center;
      font-size: 25px;
    }

    .input-label {
      display: flex;
      flex-direction: column;

      label {
        font-size: 13px;
        margin-bottom: 5px;
      }

      input {
        padding: 8px;
        border-radius: 5px;
      }
    }

    .form-actions {
      display: flex;
      align-items: center;

      button[type='submit'] {
        margin-left: auto;
      }
    }
  }
}
</style>
