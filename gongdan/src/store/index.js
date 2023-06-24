import Vue from 'vue'
import Vuex from 'vuex'
import screenfull from 'screenfull'

Vue.use(Vuex)

export default new Vuex.Store({
    namespaced: true,
    modules:{

    },
    state:{
        collapse: false,
        active: true,
        default: ''
    },
    created: {

    },
    actions: {
        /**
         * 切换折叠状态
         */
        toggle({state, commit}){
            commit('collapseSet', !state.collapse)
        },
        hide({commit}){
            commit('collapseSet', false)
        },
        /**
         * @description 切换全屏
         */
         screenToggle({ commit }) {
            return new Promise(resolve => {
            if (screenfull.isFullscreen) {
                screenfull.exit()
                commit('set', true)
            } else {
                screenfull.request()
                commit('set', false)
            }
            // end
            resolve()
            })
        }
    },
    mutations:{
        collapseSet(state, collapse) {
            state.collapse = collapse
        },
        /**
         * @description 设置 store 里的全屏状态
         * @param {Object} state vuex state
         * @param {Boolean} active active
         */
        set(state, active) {
            state.active = active
        }
    }
})