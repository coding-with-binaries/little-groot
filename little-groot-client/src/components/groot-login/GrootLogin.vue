<template>
  <div id="groot-login">
    <span id="groot-title">little groot</span>
    <div id="login-jumbotron">
      <span class="login-title">
        Login
      </span>
      <el-form
        class="login-form"
        :model="loginForm"
        :rules="rules"
        ref="loginForm"
      >
        <el-form-item label="Username" prop="userName">
          <el-input
            v-model="loginForm.userName"
            placeholder="Enter your username here"
            prefix-icon="el-icon-user"
            autofocus
          ></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="password">
          <el-input
            v-model="loginForm.password"
            placeholder="Enter your password here"
            prefix-icon="el-icon-key"
            show-password
          ></el-input>
        </el-form-item>
        <el-button
          id="login-button"
          type="primary"
          @click="submitForm('loginForm')"
        >
          Login
        </el-button>
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
  flex-direction: column;
  align-items: center;
  justify-content: center;

  #groot-title {
    margin-bottom: 2rem;

    font-size: 32px;
    font-weight: bolder;
    color: #42b983;
  }

  #login-jumbotron {
    height: 400px;
    width: 480px;

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    border: 2px solid #e8e8e8;
    border-radius: 4px;

    .login-title {
      width: 400px;
      margin-bottom: 1rem;

      text-align: start;
      font-size: 24px;
      font-weight: bold;
    }

    .login-form {
      width: 400px;
    }

    #login-button {
      width: 100%;
    }
  }
}
</style>
