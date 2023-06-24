<template>
    <div class="nm-header">
        <!--Logo-->
        <!-- <h2>工单查询</h2> -->
    </div>
</template>

<script>
    import { mapState, mapGetters, mapActions } from 'vuex'
    export default{
        data(){
            return{
                userIcon: 'el-icon-user-solid',
                logoOutIcon: 'el-icon-switch-button'
            }
        },
        computed:{
            ...mapState({sidebarCollapse: 'collapse'}),
            ...mapState({screenIcon: 'active'})
        },
        methods:{
            ...mapActions({ sidebarToggle: 'toggle' }),
            ...mapActions({ handleChange: 'screenToggle'})
            
        },
        mounted(){
            
        }
    }
</script>

<style type="text/css">
    .nm-header{
        flex-shrink:0;
        height:60px;
        line-height: 60px;
        background:#03a9f4;
        box-shadow:0 1px 1px rgb(0 0 0 / 10%);
        text-align:center;
    }
    
    h2{
        color:white;
    }

</style>