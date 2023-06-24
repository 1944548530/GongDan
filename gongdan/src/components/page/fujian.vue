<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="FJgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="FJpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="FJitem" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="FJsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:</span>&emsp;
                <el-input style="width:180px;" v-model="FJluhao" :disabled="true" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="FJgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="FJoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">刮伤:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="FJguasNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">麻点:</span>&emsp;
                <el-input style="width:180px;" v-model="FJmadNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">崩边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="FJbengbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">花边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="FJhuabNum" placeholder="请输入数字"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">脱点:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="FJtuodNum" placeholder="请输入数字"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;
                <el-input style="width:180px;" v-model="FJqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="FJkongbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="FJkongbNum" placeholder="请输入数字"></el-input>
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
                <el-button @click="FJinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                FJpinMing: '',
                FJitem: '',
                FJsize: '',
                FJluhao: '',
                FJgongdan: '',
                FJgdAmount: '',
                FJoutputNum: '',
                FJguasNum: '',
                FJmadNum: '',
                FJbengbNum: '',
                FJqitaNum: '',
                FJkongbNum: '',
                FJkongbName: '',
                FJhuabNum: '',
                FJtuodNum: '',
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
                        sn: this.FJgongdan
                    }
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        const info = result.Data
                        this.FJpinMing = info.prodName
                        this.FJitem = info.item
                        this.FJsize = info.size
                        this.FJluhao = info.potNum
                        this.FJgdAmount = info.totalAmount
                        this.hongkao = info.hongkao
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                        this.FJpinMing = ''
                        this.FJitem = ''
                        this.FJsize = ''
                        this.FJluhao = ''
                        this.FJgdAmount = ''
                    }
                    
                })
            },
            FJinfoSave: function(){
                if(this.FJpinMing == "" || this.FJitem == "" || this.FJsize == "" || this.FJluhao == "" || 
                    this.FJgongdan == "" ||  this.FJgdAmount == "" || this.FJoutputNum == "" || this.FJguasNum == ""
                    ||  this.FJmadNum == "" || this.FJbengbNum == "" || this.FJqitaNum == "" || this.FJhuabNum == "" || this.FJtuodNum == ""){
                    this.$message({
                        showClose: true,
                        message: "输入项不能为空",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('YujianMain/YujianSave',{
                        params:{
                            modular: '复检',
                            prodName: this.FJpinMing,
                            item: this.FJitem,
                            size: this.FJsize,
                            potNum: this.FJluhao,
                            snNum: this.FJgongdan,
                            totalAmount: this.FJgdAmount,
                            opAmount: this.FJoutputNum,
                            erro1: this.FJguasNum,
                            erro2: this.FJmadNum,
                            erro3: this.FJbengbNum,
                            erro4: this.FJqitaNum,
                            erro5: this.FJhuabNum,
                            erro6: this.FJtuodNum,
                            errBlankNum: this.FJkongbNum,
                            errBlankName: this.FJkongbName,
                            hongkao : this.hongkao
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
                            this.$emit('FuJianInfo', '复检')
                            this.FJpinMing = ''
                            this.FJitem = ''
                            this.FJsize = ''
                            this.FJluhao = ''
                            this.FJgongdan = ''
                            this.FJgdAmount = ''
                            this.FJoutputNum = ''
                            this.FJguasNum = ''
                            this.FJmadNum = ''
                            this.FJbengbNum = ''
                            this.FJqitaNum = ''
                            this.FJkongbNum = ''
                            this.FJkongbName = ''
                            this.FJhuabNum = ''
                            this.FJtuodNum = ''
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