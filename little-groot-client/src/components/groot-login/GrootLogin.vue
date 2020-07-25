<template>
  <div id="groot-login">
    <div id="login-jumbotron">
      <el-form
        label-position="left"
        label-width="120px"
        :model="loginForm"
        :rules="rules"
        ref="loginForm"
      >
        <el-form-item label="Username" prop="userName">
          <el-input v-model="loginForm.userName"></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="password">
          <el-input v-model="loginForm.password" show-password></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('loginForm')">
            Login
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loginForm: {
        userName: '',
        password: ''
      },
      rules: {
        userName: [
          {
            required: true,
            message: 'Username is a required field',
            trigger: 'change'
          }
        ],
        password: [
          {
            required: true,
            message: 'Password is a required field',
            trigger: 'change'
          }
        ]
      }
    };
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          alert('submit!');
        } else {
          console.log('error submit!!');
          return false;
        }
      });
    }
  }
};
</script>

<style scoped lang="scss">
#groot-login {
  height: 100%;
  width: 100%;

  display: flex;
  align-items: center;
  justify-content: center;

  #login-jumbotron {
    height: 400px;
    width: 480px;

    display: flex;
    align-items: center;
    justify-content: center;

    border: 2px solid #42b983;
    border-radius: 4px;
  }
}
</style>
