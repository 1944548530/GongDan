<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="CJgongdan"  placeholder="请输入内容" @change="snChange"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="CJpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="CJitem" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="CJsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:</span>&emsp;
                <el-input style="width:180px;" v-model="CJluhao" :disabled="true" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="CJgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="CJoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">刮伤:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="CJguasNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">麻点:</span>&emsp;
                <el-input style="width:180px;" v-model="CJmadNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">崩边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="CJbengbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">花边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="CJhuabNum" placeholder="请输入数字"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">脱点:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="CJtuodNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;
                <el-input style="width:180px;" v-model="CJqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="CJkongbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="CJkongbNum" placeholder="请输入数字"></el-input>
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
                <el-button @click="CJinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                CJpinMing: '',
                CJitem: '',
                CJsize: '',
                CJluhao: '',
                CJgongdan: '',
                CJgdAmount: '',
                CJoutputNum: '',
                CJguasNum: '',
                CJmadNum: '',
                CJbengbNum: '',
                CJqitaNum: '',
                CJkongbNum: '',
                CJkongbName: '',
                CJhuabNum: '',
                CJtuodNum: '',
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
                        sn: this.CJgongdan
                    }
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        const info = result.Data
                        this.CJpinMing = info.prodName
                        this.CJitem = info.item
                        this.CJsize = info.size
                        this.CJluhao = info.potNum
                        this.CJgdAmount = info.totalAmount
                        this.hongkao = info.hongkao
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                        this.CJpinMing = ''
                        this.CJitem = ''
                        this.CJsize = ''
                        this.CJluhao = ''
                        this.CJgdAmount = ''
                    }
                    
                })
            },
            CJinfoSave: function(){
                if(this.CJpinMing == "" || this.CJitem == "" || this.CJsize == "" || this.CJluhao == "" 
                    || this.CJgongdan == "" || this.CJgdAmount == "" || this.CJoutputNum == "" || this.CJguasNum == ""
                    || this.CJmadNum == "" || this.CJbengbNum == "" || this.CJqitaNum == "" || this.CJhuabNum == "" || this.CJtuodNum == ""){
                    this.$message({
                        showClose: true,
                        message: "输入项不能为空",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('YujianMain/YujianSave',{
                        params:{
                            modular: '初检',
                            prodName: this.CJpinMing,
                            item: this.CJitem,
                            size: this.CJsize,
                            potNum: this.CJluhao,
                            snNum: this.CJgongdan,
                            totalAmount: this.CJgdAmount,
                            opAmount: this.CJoutputNum,
                            erro1: this.CJguasNum,
                            erro2: this.CJmadNum,
                            erro3: this.CJbengbNum,
                            erro4: this.CJqitaNum,
                            erro5: this.CJhuabNum,
                            erro6: this.CJtuodNum,
                            errBlankNum: this.CJkongbNum,
                            errBlankName: this.CJkongbName,
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
                            this.$emit('ChuJianInfo', '初检')
                            this.CJpinMing = ''
                            this.CJitem = ''
                            this.CJsize = ''
                            this.CJluhao = ''
                            this.CJgongdan = ''
                            this.CJgdAmount = ''
                            this.CJoutputNum = ''
                            this.CJguasNum = ''
                            this.CJmadNum = ''
                            this.CJbengbNum = ''
                            this.CJqitaNum = ''
                            this.CJhuabNum = ''
                            this.CJtuodNum = ''
                            this.CJkongbNum = ''
                            this.CJkongbName = ''
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