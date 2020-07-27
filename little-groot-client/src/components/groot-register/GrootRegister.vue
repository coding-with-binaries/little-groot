<template>
  <div id="groot-register">
    <span id="groot-title">little groot</span>
    <div id="register-jumbotron">
      <span class="register-title">Create an account</span>
      <el-form
        class="register-form"
        :model="registerForm"
        :rules="rules"
        ref="registerForm"
        status-icon
      >
        <el-form-item label="Email Address" prop="email">
          <el-input
            v-model="registerForm.email"
            placeholder="Enter your e-mail address here"
            autofocus
          ></el-input>
        </el-form-item>
        <el-form-item label="First name" prop="firstName">
          <el-input
            v-model="registerForm.firstName"
            placeholder="Enter your first name here"
          ></el-input>
        </el-form-item>
        <el-form-item label="Last name" prop="lastName">
          <el-input
            v-model="registerForm.lastName"
            placeholder="Enter your last name here"
          ></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="password">
          <el-input
            type="password"
            v-model="registerForm.password"
            placeholder="Enter your password here"
          ></el-input>
        </el-form-item>
        <el-form-item label="Confirm Password" prop="confirmPassword">
          <el-input
            type="password"
            v-model="registerForm.confirmPassword"
            placeholder="Enter your password again"
          ></el-input>
        </el-form-item>
        <el-button id="register-button" type="primary" @click="submitForm()">
          Register
        </el-button>
      </el-form>
    </div>
    <el-button type="text" @click="routeToLogin()">
      Login to Little Groot
    </el-button>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    const validateEmailAvailability = async (rule, value, callback) => {
      const response = await axios.get(`/api/v1/users/${value}/availability`);
      if (!response.data.emailAvailable) {
        callback(new Error('The Email Address is already registered'));
      } else {
        callback();
      }
    };
    const validateConfirmPassword = (rule, value, callback) => {
      if (value !== this.registerForm.password) {
        callback(new Error('Password do not match!'));
      } else {
        callback();
      }
    };
    return {
      registerForm: {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: ''
      },
      rules: {
        firstName: [
          {
            required: true,
            message: 'First name is a required field',
            trigger: 'blur'
          }
        ],
        lastName: [
          {
            required: true,
            message: 'Last name is a required field',
            trigger: 'blur'
          }
        ],
        email: [
          {
            required: true,
            message: 'Email is a required field',
            trigger: 'blur'
          },
          {
            type: 'email',
            message: 'Email address is not valid',
            trigger: 'blur'
          },
          { validator: validateEmailAvailability, trigger: 'blur' }
        ],
        password: [
          {
            required: true,
            message: 'Password is a required field',
            trigger: 'blur'
          },
          {
            min: 6,
            message: 'Password should have minimum 6 characters',
            trigger: 'blur'
          }
        ],
        confirmPassword: [
          {
            required: true,
            message: 'Password Confirmation is required',
            trigger: 'blur'
          },
          { validator: validateConfirmPassword, trigger: 'blur' }
        ]
      }
    };
  },
  methods: {
    submitForm() {
      this.$refs.registerForm.validate(valid => {
        if (valid) {
          const payload = { ...this.registerForm };
          delete payload.confirmPassword;
          this.registerUser(payload);
        } else {
          return false;
        }
      });
    },
    async registerUser(payload) {
      try {
        await axios.post('/api/v1/users/register', payload);
        this.$router.push('/login');
      } catch (e) {
        //
      }
    },
    routeToLogin() {
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped lang="scss">
#groot-register {
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

  #register-jumbotron {
    padding: 4rem;

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    border: 2px solid #e8e8e8;
    border-radius: 4px;

    .register-title {
      width: 400px;
      margin-bottom: 1rem;

      text-align: start;
      font-size: 24px;
      font-weight: bold;
    }

    .register-form {
      width: 400px;
    }

    #register-button {
      width: 100%;
    }
  }
}
</style>
