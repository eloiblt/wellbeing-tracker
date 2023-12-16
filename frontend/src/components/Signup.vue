<script setup lang="ts">
import { inject, ref } from 'vue';
import type ApiClient from '../services/api.service.ts';
import type AuthService from '../services/auth.service.ts';
import { validateEmail } from '../services/check-email.service.ts';
import { useI18n } from 'vue-i18n';

const authService = inject<AuthService>('authService') as AuthService;
const apiClient = inject<ApiClient>('apiClient') as ApiClient;
const { t } = useI18n();

const firstName = ref<string>();
const lastName = ref<string>();
const email = ref<string>();
const password = ref<string>();
const errors = ref<string[]>([]);

async function signup() {
  validateForm();
  if (errors.value.length) return;

  apiClient
    .signup({ firstName: firstName.value, lastName: lastName.value, email: email.value!, password: password.value! })
    .subscribe({
      next: async res => {
        await authService.login(res.data);
        error.value = false;
      },
      error: () => {
        error.value = true;
      },
    });
}

function validateForm() {
  errors.value = [];
  if (!validateEmail(email.value)) {
    errors.value.push('email');
  }

  if (password.value?.trim().length < 12) {
    errors.value.push('password');
  }
}

function formDisabled() {
  return !firstName.value?.trim() || !lastName.value?.trim() || !email.value?.trim() || !password.value?.trim();
}
</script>

<template>
  <div class="login-container">
    <form v-on:submit.prevent="signup">
      <h1>Signup</h1>

      <p class="error" v-for="(error, index) in errors" :key="index">{{ t(error) }}</p>

      <div class="input-label">
        <label>First name</label>
        <input type="text" v-model="firstName" />
      </div>

      <div class="input-label">
        <label>Last name</label>
        <input type="text" v-model="lastName" />
      </div>

      <div class="input-label">
        <label>Email</label>
        <input type="email" v-model="email" />
      </div>

      <div class="input-label">
        <label>Password</label>
        <input type="password" v-model="password" />
      </div>

      <div class="form-actions">
        <router-link class="app-name-container" to="/login">
          <button type="button" class="btn-no-style">Login</button>
        </router-link>
        <button class="btn-primary" :disabled="formDisabled()" type="submit">Signup</button>
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
        //border: none;
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
