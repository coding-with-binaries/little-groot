<template>
  <div class="home">
    <groot-header></groot-header>
  </div>
</template>

<script>
import { mutations } from '@/store';
import UserApi from '@/api/User';
import GrootHeader from '@/components/groot-header/GrootHeader.vue';

export default {
  created: async function() {
    const user = await UserApi.getCurrentUser();
    mutations.updateUser(user);
  },
  components: {
    GrootHeader
  }
};
</script>
