<template>
  <div id="little-groot">
    <GrootHeader />
    <router-view />
  </div>
</template>

<script lang="ts">
import GrootHeader from '@/components/groot-header/GrootHeader.vue';

export default {
  name: 'Home',
  components: {
    GrootHeader
  }
};
</script>

<style lang="scss">
body {
  margin: 0;
}
#little-groot {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
