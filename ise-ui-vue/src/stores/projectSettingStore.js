import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

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
                var url = import.meta.env.VITE_ISE_API_URL;
                var path = import.meta.env.VITE_ISE_API_CATEGORIES;
                this.categories = await fetch(url+path)
                .then((response) => response.json())
            } catch (error) {
                this.error = error
            } finally {
                this.loading = false
            }
        }
    }
})