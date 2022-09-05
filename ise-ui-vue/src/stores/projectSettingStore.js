import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
import coreapi from '../services/coreapi';

export const useProjectSettingStore = defineStore('projectSetting', {
    state: () => ({
        categories:[],
        loading: false,
        error: null
    }),
    getters: {
    },
    actions: {
        async FetchCategories() {
            this.categories = []
            this.loading = true
            try {
                this.categories = await coreapi.getProjectCategories();
            } catch (error) {
                this.error = error
            } finally {
                this.loading = false
            }
        }
    }
})