<script setup lang="ts">
import { inject, onBeforeMount, onMounted, ref } from 'vue';
import type ApiClient from '../services/api.service.ts';

const apiClient = inject<ApiClient>('apiClient') as ApiClient;
const apparitionSpeed = 0.03;

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

onMounted(() => {
  apiClient.getUserById().subscribe(res => {
    console.log(res.data);
  });
});
</script>

<template></template>

<style scoped lang="scss">
.card-container {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;

  .card {
    animation-name: appear;
    opacity: 0;
    animation-duration: 1s;
    animation-fill-mode: forwards;

    .card-text-wrapper {
      display: flex;
      flex-direction: column;
      gap: 7px;
    }
  }
}

@keyframes appear {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
</style>
