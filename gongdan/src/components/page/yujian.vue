<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="YJgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="YJpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="YJitem" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="YJsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:</span>&emsp;
                <el-input style="width:180px;" v-model="YJluhao" :disabled="true" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="YJgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="YJoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">刮伤:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="YJguasNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">麻点:</span>&emsp;
                <el-input style="width:180px;" v-model="YJmadNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">崩边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="YJbengbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">花边:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="YJhuabNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">脱点:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="YJtuodNum" placeholder="请输入数字"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;
                <el-input style="width:180px;" v-model="YJqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="YJkongbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="YJkongbNum" placeholder="请输入数字"></el-input>
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
                <el-button @click="YJinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                YJpinMing: '',
                YJitem: '',
                YJsize: '',
                YJluhao: '',
                YJgongdan: '',
                YJgdAmount: '',
                YJoutputNum: '',
                YJguasNum: '',
                YJmadNum: '',
                YJbengbNum: '',
                YJqitaNum: '',
                YJkongbNum: '',
                YJkongbName: '',
                YJhuabNum: '',
                YJtuodNum: '',
                hongkao: '烘烤',
                hongkaoOption:[
                    {value: '烘烤', label: '烘烤'},
                    {value: '未烘烤', label: '未烘烤'}
                ],
            }
        },
        created: function(){
            //this.$emit('YujianInfo', '预检')
        },
        computed:{
            
        },
        methods:{
            snChange: function(){
                this.$http.get('QingxiMain/GetInfoBySn', {
                    params:{
                        sn: this.YJgongdan
                    }
                }).then(res => {
                    const result = res.data
                    if(result.code == 200){
                        const info = result.Data
                        this.YJpinMing = info.prodName
                        this.YJitem = info.item
                        this.YJsize = info.size
                        this.YJluhao = info.potNum
                        this.YJgdAmount = info.totalAmount
                        this.hongkao = info.hongkao
                    }else{
                        this.$message({
                            showClose: true,
                            message: result.message,
                            type: 'error',
                            duration: 2000,
                        })
                        this.YJpinMing = ''
                        this.YJitem = ''
                        this.YJsize = ''
                        this.YJluhao = ''
                        this.YJgdAmount = ''
                    }
                    
                })
            },
            YJinfoSave: function(){
                if(this.YJpinMing == "" || this.YJitem == "" || this.YJsize == "" || this.YJluhao == "" 
                    || this.YJgongdan == "" || this.YJgdAmount == "" || this.YJoutputNum == "" || this.YJguasNum == "" 
                    ||  this.YJmadNum == "" || this.YJbengbNum == "" || this.YJqitaNum == "" || this.YJhuabNum == "" || this.YJtuodNum == ""){
                    this.$message({
                        showClose: true,
                        message: "输入项不能为空",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('YujianMain/YujianSave',{
                        params:{
                            modular: '预检',
                            prodName: this.YJpinMing,
                            item: this.YJitem,
                            size: this.YJsize,
                            potNum: this.YJluhao,
                            snNum: this.YJgongdan,
                            totalAmount: this.YJgdAmount,
                            opAmount: this.YJoutputNum,
                            erro1: this.YJguasNum,
                            erro2: this.YJmadNum,
                            erro3: this.YJbengbNum,
                            erro4: this.YJqitaNum,
                            erro5: this.YJhuabNum,
                            erro6: this.YJtuodNum,
                            errBlankNum: this.YJkongbNum,
                            errBlankName: this.YJkongbName,
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
                            this.$emit('YujianInfo', '预检')
                            this.YJpinMing = ''
                            this.YJitem = ''
                            this.YJsize = ''
                            this.YJluhao = ''
                            this.YJgongdan = ''
                            this.YJgdAmount = ''
                            this.YJoutputNum = ''
                            this.YJguasNum = ''
                            this.YJmadNum = ''
                            this.YJbengbNum = ''
                            this.YJqitaNum = ''
                            this.YJkongbNum = ''
                            this.YJkongbName = ''
                            this.YJhuabNum = ''
                            this.YJtuodNum = ''
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
    ::v-deep .el-input{
        width:180px;
    }

</style>