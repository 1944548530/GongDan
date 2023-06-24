<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="QXgongdan" placeholder="请输入内容" @change="snChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="QXgongdErrShow">格式不对</span>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">品名:&emsp;&emsp;</span>
                <el-input style="width:180px;" v-model="QXpinMing" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="QXitem" placeholder="请输入内容" :disabled="true" @change="QXitemChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="QXitemErrShow">格式不对</span>
                
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">尺寸:</span>
                <el-input style="width:180px;" v-model="QXsize" :disabled="true" placeholder="请输入内容"></el-input>
                
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">炉号:&emsp;</span>
                <el-input style="width:180px;" v-model="QXluhao" :disabled="true" placeholder="请输入内容" @change="GXluhaoChange"></el-input>
                <span style="color:red;font-size:10px;" v-show="QXluhaoErrShow">格式不对</span>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">工单总量:</span>
                    <el-input style="width:180px;" v-model="QXgdAmount" :disabled="true" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">良品数量:</span>
                    <el-input style="width:180px;" v-model="QXoutputNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">刮伤:</span>
                <el-input style="width:180px;" v-model="QXguasNum" placeholder="请输入数字"></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="5">
                <span class="inpTitle">丢片:</span>&emsp;
                <el-input style="width:180px;" v-model="QXdiuPianNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <span class="inpTitle">其他:</span>&emsp;&emsp;
                <el-input style="width:180px;" v-model="QXqitaNum" placeholder="请输入数字"></el-input>
            </el-col>
            <el-col :span="5">
                <el-input style="width:100px;" v-model="QXkbName" placeholder="不良名称"></el-input>
                <el-input style="width:150px;" v-model="QXkbNum" placeholder="请输入数字"></el-input>
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
            
        </el-row>
        <el-row>
            <el-col :span="24" style="text-align:center;">
                <el-button @click="QXinfoSave" size="medium" type="primary">保存</el-button>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                QXpinMing: '',
                QXitem: '',
                QXsize: '',
                QXluhao: '',
                QXgongdan: '',
                QXgdAmount: '',
                QXoutputNum: '',
                QXguasNum: '',
                QXdiuPianNum: '',
                QXqitaNum: '',
                QXitemErrShow: false,
                QXluhaoErrShow: false,
                QXgongdErrShow: false,
                QXkbName: '',
                QXkbNum: '',
                hongkao: '烘烤',
                hongkaoOption:[
                    {value: '烘烤', label: '烘烤'},
                    {value: '未烘烤', label: '未烘烤'}
                ],
            }
        },
        created: function(){
            //this.$emit('QingxiInfo', '清洗')
        },
        computed:{
            
        },
        methods:{
            QXitemChange: function(){
                if(this.QXitem.length != 7){
                    this.QXitemErrShow = true
                }else{
                    this.QXitemErrShow = false
                }
            },
            GXluhaoChange: function(){
                if(this.QXluhao.length != 15){
                    this.QXluhaoErrShow = true
                }else{
                    this.QXluhaoErrShow = false
                }
            },
            snChange: function(){
                if(this.QXgongdan.length != 12){
                    this.QXgongdErrShow = true
                }else{
                    this.QXgongdErrShow = false
                    this.$http.get('QingxiMain/GetInfoBySn', {
                        params:{
                            sn: this.QXgongdan
                        }
                    }).then(res => {
                        const result = res.data
                        if(result.code == 200){
                            const info = result.Data
                            this.QXpinMing = info.prodName
                            this.QXitem = info.item
                            this.QXsize = info.size
                            this.QXluhao = info.potNum
                            this.QXgdAmount = info.totalAmount
                            this.hongkao = info.hongkao
                        }else{
                            this.$message({
                                showClose: true,
                                message: result.message,
                                type: 'error',
                                duration: 2000,
                            })
                            this.QXpinMing = ''
                            this.QXitem = ''
                            this.QXsize = ''
                            this.QXluhao = ''
                            this.QJtestNum = ''
                        }
                        
                    })
                }
                
            },
            QXinfoSave: function(){
                if(this.QXpinMing == "" || this.QXitem == "" || this.QXsize == "" || this.QXluhao == "" || 
                    this.QXgongdan == "" || this.QXgdAmount == "" || this.QXoutputNum == "" || this.QXitemErrShow == true
                    || this.QXluhaoErrShow == true || this.QXgongdErrShow == true || this.QXguasNum == '' || this.QXdiuPianNum == ''){
                    this.$message({
                        showClose: true,
                        message: "请确认输入项不能为空且格式正确",
                        type: 'error',
                        duration: 2000,
                    })
                }else{
                    this.$http.get('QingxiMain/QingxiSave',{
                        params:{
                            prodName: this.QXpinMing,
                            item: this.QXitem,
                            size: this.QXsize,
                            potNum: this.QXluhao,
                            snNum: this.QXgongdan,
                            totalAmount: this.QXgdAmount,
                            opAmount: this.QXoutputNum,
                            erro1: this.QXguasNum,
                            erro2: this.QXdiuPianNum,
                            erro3: this.QXqitaNum,
                            errBlankName: this.QXkbName,
                            errBlankNum: this.QXkbNum,
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
                            this.$emit('QingxiInfo', '清洗')
                            this.QXpinMing = ''
                            this.QXitem = ''
                            this.QXsize = ''
                            this.QXluhao = ''
                            this.QXgongdan = ''
                            this.QXgdAmount = ''
                            this.QXoutputNum = ''
                            this.QXguasNum = ''
                            this.QXdiuPianNum = ''
                            this.QXqitaNum = ''
                            this.QXkbName = ''
                            this.QXkbNum = ''
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