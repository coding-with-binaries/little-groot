<template>
  <div id="groot-login">
    <span id="groot-title">little groot</span>
    <div id="login-jumbotron">
      <span class="login-title">Login</span>
      <el-form
        class="login-form"
        :model="loginForm"
        :rules="rules"
        ref="loginForm"
      >
        <el-form-item label="Email Address" prop="email">
          <el-input
            v-model="loginForm.email"
            placeholder="Enter your e-mail address here"
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
        <el-button id="login-button" type="primary" @click="submitForm()">
          Login
        </el-button>
        <el-checkbox class="remember-me" v-model="loginForm.rememberMe">
          Remember me
        </el-checkbox>
      </el-form>
    </div>
    <el-button type="text" @click="routeToRegister()">
      Register to Little Groot
    </el-button>
  </div>
</template>

<script>
import UserApi from '../../api/User';

export default {
  data() {
    return {
      loginForm: {
        email: '',
        password: '',
        rememberMe: false
      },
      rules: {
        email: [
          {
            required: true,
            message: 'Email address is a required field',
            trigger: 'blur'
          },
          {
            type: 'email',
            message: 'Email address is not valid',
            trigger: 'blur'
          }
        ],
        password: [
          {
            required: true,
            message: 'Password is a required field',
            trigger: 'blur'
          }
        ]
      }
    };
  },
  methods: {
    submitForm() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          const payload = this.loginForm;
          this.authenticateUser(payload);
        } else {
          console.log('error submit!!');
          return false;
        }
      });
    },
    async authenticateUser(payload) {
      try {
        const response = await UserApi.authenticate(payload);
        if (response.status === 200) {
          localStorage.setItem('groot-auth-token', response.data.token);
          this.$router.push('/');
        }
      } catch (e) {
        // Handle Error
      }
    },
    routeToRegister() {
      this.$router.push('/register');
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
    padding: 4rem;

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

      .remember-me {
        margin-top: 1rem;
      }
    }

    #login-button {
      width: 100%;
    }
  }
}
</style>
