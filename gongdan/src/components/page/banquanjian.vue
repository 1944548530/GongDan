<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">炉号:</span>&emsp;&emsp;
                    <el-input style="width:180px;" v-model="QJluhao" placeholder="请输入内容" @change="luhaoChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="luhaoErrShow">格式不对</span>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="QJitem" placeholder="请输入内容" @change="itemChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="itemErrShow">格式不对</span>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">尺寸:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="QJsize" placeholder="请输入内容"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">品名:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="QJpinMing" placeholder="请输入内容"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">测试数量:</span>
                <el-input style="width:180px;" v-model="QJtestNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">不良数量:</span>
                <el-input style="width:180px;" v-model="QJerroNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">良品数量:</span>
                <el-input style="width:180px;" v-model="QJoutputNum" placeholder="请输入数字"></el-input>
                
            </el-col>
            <el-col :span="6">
                <span class="inpTitle">短需烘烤:</span>
                <el-input style="width:180px;" v-model="QJhkNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="6">
                <span class="inpTitle">其他:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="QJqtNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <el-input style="width:100px;" v-model="QJkbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="QJkbNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="6">
                <el-button @click="QJinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                QJpinMing: '',
                QJitem: '',
                QJsize: '',
                QJluhao: '',
                QJtestNum: '',
                QJerroNum: '',
                QJhkNum: '',
                QJqtNum: '',
                QJkbNum: '',
                itemErrShow: false,
                luhaoErrShow: false,
                QJkbName: '',
                QJoutputNum: ''
            }
        },
        computed:{
            
        },
        methods:{
            itemChange: function(){
                if(this.QJitem.length != 7){
                    this.itemErrShow = true
                }else{
                    this.itemErrShow = false
                }
            },
            luhaoChange: function(){
                if(this.QJluhao.length != 15){
                    this.luhaoErrShow = true
                }else{
                    this.luhaoErrShow = false
                    this.$http.get('QuanjMain/InfoByLuhao', {
                        params:{
                            luhao: this.QJluhao
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            const info = result.Data
                            this.QJpinMing = info.prodName
                            this.QJitem = info.item
                            this.QJsize = info.size
                            this.QJtestNum = info.totalAmount
                            this.QJerroNum = info.badNum
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                            this.QJpinMing = ''
                            this.QJitem = ''
                            this.QJsize = ''
                            this.QJtestNum = ''
                            this.QJerroNum = ''
                        }
                        
                    })
                }
            },
            QJinfoSave: function(){
                if(this.QJpinMing == "" || this.QJitem == "" || this.QJsize == "" || this.QJluhao == ""
                    || this.QJtestNum == "" || this.QJerroNum == "" || this.QJhkNum == "" || this.QJqtNum == ""
                    || this.itemErrShow == true || this.luhaoErrShow == true){
                        this.$message({
                            showClose: true,
                            message: "请确认输入项不能为空且格式正确",
                            type: 'error',
                            duration: 2000,
                        })
                }else{
                    console.log(this.QJkbName)
                    this.$http.get('/QuanjMain/QuanjSave',{
                        params:{
                            prodName: this.QJpinMing,
                            item: this.QJitem,
                            size: this.QJsize,
                            potNum: this.QJluhao,
                            totalAmount: this.QJtestNum,
                            badNum: this.QJerroNum,
                            opAmount: this.QJoutputNum,
                            erro1: this.QJhkNum,
                            erro2: this.QJqtNum,
                            errBlankName: this.QJkbName,
                            errBlankNum: this.QJkbNum
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
                            this.$emit('QuanjInfo')
                            this.QJpinMing = ''
                            this.QJitem = ''
                            this.QJsize = ''
                            this.QJluhao = ''
                            this.QJtestNum = ''
                            this.QJerroNum = ''
                            this.QJhkNum = ''
                            this.QJqtNum = ''
                            this.QJkbNum = ''
                            this.QJkbName = ''
                            this.QJoutputNum = ''
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