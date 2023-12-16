<script setup lang="ts">
const props = defineProps({
  open: {
    type: Boolean,
    default: false,
  },
});
</script>

<template>
  <div class="menu" :class="{ open: props.open }">
    <router-link to="/">
      <h2>Home</h2>
    </router-link>
    <router-link to="/journal">
      <h2>Journal</h2>
    </router-link>
    <router-link to="/metrics">
      <h2>Metrics</h2>
    </router-link>
  </div>
</template>

<style scoped lang="scss">
.menu {
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  width: 0;
  background-color: var(--background-primary);
  z-index: 1;
  transition: all 0.3s;
  overflow: hidden;
  display: flex;
  flex-direction: column;
  padding: 20px 0;
  filter: drop-shadow(5px 0px 5px var(--drop-shadow));

  h2 {
    padding: 10px 20px;
    cursor: pointer;
  }

  &.open {
    width: 170px;
  }
}
</style>
