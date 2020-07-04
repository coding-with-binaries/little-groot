<template>
  <input @input="handleInputValue($event)" />
</template>

<script>
import { Component, Prop, Vue, Emit } from 'vue-property-decorator';

@Component
export default class GrootInput extends Vue {
  @Prop() value;

  @Emit()
  handleInputValue($event) {
    return $event.target.value;
  }
}
</script>
