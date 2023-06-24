<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="FXNgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="FXNpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="FXNitem" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="FXNsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:</span>&emsp;
                <el-input style="width:180px;" v-model="FXNluhao" :disabled="true" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="FXNgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="FXNoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">短需烘烤:</span>
                <el-input style="width:180px;" v-model="FXNduanNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;
                <el-input style="width:180px;" v-model="FXNqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="FXNkongbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="FXNkongbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-select v-model="hongkao" placeholder="请选择" disabled>
                    <el-option
                        v-for="item in hongkaoOption"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                    >
                    </el-option>
                </el-select>
            </el-col>
            <el-col :span="5">
                <el-button @click="FXNinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                FXNpinMing: '',
                FXNitem: '',
                FXNsize: '',
                FXNluhao: '',
                FXNgongdan: '',
                FXNgdAmount: '',
                FXNoutputNum: '',
                FXNduanNum: '',
                FXNqitaNum: '',
                FXNkongbNum: '',
                FXNkongbName: '',
                hongkao: '烘烤',
                hongkaoOption:[
                    {value: '烘烤', label: '烘烤'},
                    {value: '未烘烤', label: '未烘烤'}
                ],
            }
        },
        computed:{
            
        },
        methods:{
            snChange: function(){
                this.$http.get('QingxiMain/GetInfoBySn', {
                    params:{
                        sn: this.FXNgongdan
                    }
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        const info = result.Data
                        this.FXNpinMing = info.prodName
                        this.FXNitem = info.item
                        this.FXNsize = info.size
                        this.FXNluhao = info.potNum
                        this.FXNgdAmount = info.totalAmount
                        this.hongkao = info.hongkao
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                        
                        this.FXNpinMing = ''
                        this.FXNitem = ''
                        this.FXNsize = ''
                        this.FXNluhao = ''
                        this.FXNgdAmount = ''
                    }
                    
                })
            },
            FXNinfoSave: function(){
                if(this.FXNpinMing == "" || this.FXNitem == "" || this.FXNsize == "" || this.FXNluhao == ""
                    || this.FXNgongdan == "" || this.FXNgdAmount == "" || this.FXNoutputNum == "" || this.FXNduanNum == "" 
                    || this.FXNqitaNum == ""){
                    this.$message({
                        showClose: true,
                        message: "输入项不能为空",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('XingNeng/FujianXNSave',{
                        params:{
                            prodName: this.FXNpinMing,
                            item: this.FXNitem,
                            size: this.FXNsize,
                            potNum: this.FXNluhao,
                            snNum: this.FXNgongdan,
                            totalAmount: this.FXNgdAmount,
                            opAmount: this.FXNoutputNum,
                            erro1: this.FXNduanNum,
                            erro2: this.FXNqitaNum,
                            errBlankNum: this.FXNkongbNum,
                            errBlankName: this.FXNkongbName,
                            hongkao: this.hongkao
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            this.$message({
                                showClose: true,
                                message: '保存成功',
                                type: 'success',
                                duration: 2000,
                            })
                            this.$emit('TaoXNInfo', '套圈性能')
                            this.FXNpinMing = ''
                            this.FXNitem = ''
                            this.FXNsize = ''
                            this.FXNluhao = ''
                            this.FXNgongdan = ''
                            this.FXNgdAmount = ''
                            this.FXNoutputNum = ''
                            this.FXNduanNum = ''
                            this.FXNqitaNum = ''
                            this.FXNkongbNum = ''
                            this.FXNkongbName = ''
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                        }
                    })
                }
                
            }
            
        }
    }
</script>

<style>
</style>