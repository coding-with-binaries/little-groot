import Vue from 'vue';
import { Store } from './types';
import { User } from './types/User';

export const store = Vue.observable<Store>({
  user: null
});

export const mutations = {
  updateUser(user: User) {
    store.user = user;
  }
};
