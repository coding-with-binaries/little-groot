<template>
  <div id="groot-header">
    <div class="groot-categories"></div>
    <span class="groot-title">Little Groot</span>
    <div class="groot-user-options"></div>
  </div>
</template>

<script>
import { store } from '@/store';
export default {
  created: function() {
    console.log(store);
  }
};
</script>

<style scoped lang="scss">
#groot-header {
  height: 4rem;

  display: grid;
  grid-template-columns: 1fr 10rem 1fr;
  align-items: center;

  background-color: #42b983;
  color: white;

  .groot-categories {
    display: inline-flex;
    align-items: center;
    justify-content: center;
  }

  .groot-title {
    font-size: x-large;
    cursor: pointer;
  }
}
</style>
