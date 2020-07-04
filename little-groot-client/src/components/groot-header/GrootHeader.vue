<template>
  <div id="groot-header">
    <div class="groot-categories"></div>
    <span class="groot-title">little-groot</span>
    <div class="groot-user-options"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import GrootInput from '../groot-input-components/groot-input/GrootInput.vue';

@Component({ components: { GrootInput } })
export default class GrootHeader extends Vue {
  private value!: string;
}
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
