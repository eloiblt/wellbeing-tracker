<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps({
  size: {
    type: String,
    default: '40px',
  },
});

const styles = computed(() => {
  return {
    width: props.size,
    height: props.size,
  };
});
</script>

<template>
  <div class="loader-container">
    <svg :style="styles" class="spinner spinner--circle" viewBox="0 0 66 66" xmlns="http://www.w3.org/2000/svg">
      <circle class="path" cx="33" cy="33" fill="none" r="30" stroke-linecap="round" stroke-width="6"></circle>
    </svg>
  </div>
</template>

<style lang="scss" scoped>
$offset: 187;
$duration: 1.4s;

.loader-container {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 10000;

  .spinner {
    animation: circle-rotator $duration linear infinite;

    * {
      line-height: 0;
      box-sizing: border-box;
    }

    .path {
      stroke-dasharray: $offset;
      stroke-dashoffset: 0;
      transform-origin: center;
      animation: circle-dash $duration ease-in-out infinite;
      stroke: var(--primary-color);
    }
  }
}

@keyframes circle-rotator {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(270deg);
  }
}

@keyframes circle-dash {
  0% {
    stroke-dashoffset: $offset;
  }
  50% {
    stroke-dashoffset: calc($offset/4);
    transform: rotate(135deg);
  }
  100% {
    stroke-dashoffset: $offset;
    transform: rotate(450deg);
  }
}
</style>
