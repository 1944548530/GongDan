<template>
    <div class="procDiv">
        <el-row>
            <el-col :span="24">
                <span class="inpTitle">工序:</span>
                <el-select v-model="procedure" clearable placeholder="请选择">
                    <el-option
                        v-for="item in procedureOption"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                    >
                    </el-option>
                </el-select>&emsp;
                <span class="inpTitle">工单号:</span>
                <el-input style="width:180px;" v-model="gongdan" placeholder="请输入工单"></el-input>&emsp;
                <span class="inpTitle">成品料号:</span>
                <el-input style="width:180px;" v-model="liaohao" placeholder="请输入成品料号"></el-input>&emsp;
                <span class="inpTitle">炉号:</span>
                <el-input style="width:180px;" v-model="luhao" placeholder="请输入炉号"></el-input>&emsp;
                <el-button type="primary" icon="el-icon-search" size="medium" @click="gongdanBtn">查询</el-button>&emsp;
            </el-col>
        </el-row>

        <el-table :data="infoList" border stripe style="width: 100%;margin-left:2%;" :header-cell-style="{background:'#f5f7fa'}">
            <el-table-column type="index"></el-table-column>
            <el-table-column label="日期" align="center" header-align="center" width="90px" prop="date"></el-table-column>
            <el-table-column label="工单号" align="center" header-align="center" width="120px" prop="snNum"></el-table-column>
            <el-table-column label="工序" align="center" header-align="center" width="80px" prop="modular"></el-table-column>
            <el-table-column label="品名" align="center" header-align="center" width="110px" prop="prodName"></el-table-column>
            <el-table-column label="成品料号" align="center" header-align="center" width="110px" prop="item"></el-table-column>
            <el-table-column label="尺寸" align="center" header-align="center" width="80px" prop="size"></el-table-column>
            <el-table-column label="炉号" align="center" header-align="center" width="130px" prop="potNum"></el-table-column>
            <el-table-column label="工单总量" align="center" header-align="center" width="80px" prop="totalAmount"></el-table-column>
            <el-table-column label="剩余数量" align="center" header-align="center" width="80px" prop="leftNum"></el-table-column>
            <el-table-column label="不良总量" align="center" header-align="center" width="80px" prop="erroTotal"></el-table-column>
            <el-table-column label="前工序良品总量" align="center" header-align="center" width="80px" prop="lastProcOKNum"></el-table-column>
            <el-table-column label="总不良率" align="center" header-align="center" width="80px" prop="erroTotalPer"></el-table-column>
            <el-table-column label="投入数量" align="center" header-align="center" width="80px" prop="inputAmount"></el-table-column>
            <el-table-column label="良品数量" align="center" header-align="center" width="80px" prop="opAmount"></el-table-column>
            <el-table-column label="不良现象" align="center" header-align="center" width="80px" prop="erroNum"></el-table-column>
            <el-table-column label="不良率" align="center" header-align="center" width="80px" prop="erroPer"></el-table-column>
            <el-table-column label="不良现象明细" align="left" header-align="center" prop="erroDetail"></el-table-column>
            <el-table-column label="烘烤" align="center" header-align="center" prop="hongkao"></el-table-column>
        </el-table>
        <el-pagination
                :current-page="pageNum"
                :page-size="pagesize"
                :page-sizes="[5,10]"
                :total="total"
                @current-change="handleCurrentChange"
                @size-change="handleSizeChange"
                background
                layout="prev, pager, next, jumper, sizes, total"
        ></el-pagination>
    </div>
</template>

<script>
    export default{
        data(){
            return{
                gongdan: '',
                procedure: '',
                liaohao: '',
                luhao: '',
                infoList:[],
                procedureOption:[
                    {value: '半成品全检', label: '半成品全检'},
                    {value: '滚圆', label: '滚圆'},
                    {value: '清洗', label: '清洗'},
                    {value: '预检', label: '预检'},
                    {value: '裸片性能', label: '裸片性能'},
                    {value: '初检', label: '初检'},
                    {value: '套圈性能', label: '套圈性能'},
                    {value: '复检', label: '复检'}
                ],
                pageNum: 1,
                pagesize: 10,
                total: 0
            }
        },
        computed:{
            
        },
        methods:{
            gongdanBtn: function(){
                this.getInfoList()
            },
            getInfoList: function(){
                this.$http.get('ChaXun/InfoBySn',{
                    params:{
                        pageNum: this.pageNum,
                        pagesize: this.pagesize,
                        gongdan: this.gongdan,
                        procedure: this.procedure,
                        liaohao: this.liaohao,
                        luhao: this.luhao
                    }
                }).then(res => {
                    const result = res.data
                    this.infoList = result.rows
                    this.total = result.total
                })
            },
            handleCurrentChange: function(newPage){
                this.pageNum = newPage
                this.getInfoList()
            },
            handleSizeChange: function(newSize){
                this.pagesize = newSize
                this.getInfoList()
            }
        }
    }
</script>

<style>
</style>