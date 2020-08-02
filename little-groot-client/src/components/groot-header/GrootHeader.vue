<template>
  <div id="groot-header">
    <div class="groot-categories"></div>
    <span class="groot-title">little groot</span>
    <div class="groot-user-options">
      <el-dropdown class="groot-user-dropdown" @command="handleCommand">
        <span class="el-dropdown-link">
          Hi {{ firstName }}!
          <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item
            v-if="!firstName"
            icon="el-icon-user"
            command="login"
          >
            Login/Register
          </el-dropdown-item>
          <el-dropdown-item
            v-if="firstName"
            icon="el-icon-user"
            command="logout"
          >
            Logout
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </div>
</template>

<script>
import { store, mutations } from '@/store';
import { clearAuthorizationToken } from '../../utils/api-utils';
export default {
  computed: {
    firstName() {
      return store.user ? store.user.firstName : '';
    }
  },
  methods: {
    handleCommand(command) {
      if (command === 'login') {
        this.$router.push('/login');
      } else if (command === 'logout') {
        clearAuthorizationToken();
        mutations.updateUser(null);
      }
    }
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

  .groot-user-options {
    display: inline-flex;
    align-items: center;
    justify-content: flex-end;

    .groot-user-dropdown {
      display: flex;
      align-items: center;
      justify-content: flex-start;
      margin-right: 1rem;

      cursor: pointer;

      color: white;
      font-size: 16px;
    }
  }
}
</style>
