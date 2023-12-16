<script setup lang="ts">
import { inject, onBeforeMount, onMounted, ref } from 'vue';
import type ApiClient from '../services/api.service.ts';
import { firstValueFrom } from 'rxjs';

const apiClient = inject<ApiClient>('apiClient') as ApiClient;
interface CardItem {
  id: number;
  cardTitle: string;
  cardManagerName: string;
  cardPhone: string;
  cardAddress: string;
}

const cardItems = ref<CardItem[]>([]);

onBeforeMount(() => {
  setTimeout(() => {
    cardItems.value = [
      ...Array(10).fill({
        id: 0,
        cardTitle: 'Network Manager',
        cardManagerName: 'Gilles Drouin',
        cardPhone: '(514) 870-7772',
        cardAddress: '671 rue de Lagauchetière Ouest1',
      }),
      ...Array(10).fill({
        id: 0,
        cardTitle: 'Roles Manager',
        cardManagerName: 'Eloi Bellet',
        cardPhone: '(514) 870-7772',
        cardAddress:
          '671 rue de Lagauchetière Ouest1 671 rue de Lagauchetière Ouest1671 rue de Lagauchetière Ouest1671 rue de Lagauchetière Ouest1671 rue de Lagauchetière Ouest1',
      }),
    ];
  }, 2000);
});

onMounted(async () => {
  await request();
});

async function request() {
  const user = (await firstValueFrom(apiClient.getCurrentUser())).data;
  console.log(user);
}
</script>

<template>
  <button type="button" @click="request">Authenticated request</button>
</template>

<style scoped lang="scss"></style>
