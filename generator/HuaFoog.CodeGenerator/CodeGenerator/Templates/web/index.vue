<template>
    <div>
        <el-form :inline="true" :model="search" class="demo-form-inline">
            <!--<el-form-item label="审批人">
        <el-input v-model="search.user" placeholder="审批人" />
    </el-form-item>
    <el-form-item>
        <el-button type="primary" @click="onSearch">查询</el-button>
    </el-form-item>-->
        </el-form>
        <el-table border :data="list">
            $foreach(item in model.ColumnConfig)
            $if(item.PropName == "CreateTime")
            ${ elif(item.PropName == "DeleteTime")}
            ${ elif(item.PropName == "CreatedId")}
            ${ elif(item.PropName == "Id")}
            $else
            <el-table-column prop="${item.PropName}" label="${item.Remark}" />
            $end
            $end
            <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button type="text" @click="handleEdit(scope.row)">编辑</el-button>
                    <el-button slot="reference" type="text" @click="handleDelete(scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <div class="common-pagination">
            <el-pagination :page-size="page.pageSize"
                           :current-page="page.pageNo"
                           background
                           layout="prev, pager, next"
                           :page-sizes="[10, 20, 30, 40,50]"
                           :total="page.total"
                           @current-change="pageChange"
                           @size-change="sizeChange" />
        </div>
        <AddOrUpdate v-if="showDetail" :show.sync="showDetail" :types="types" :init-params="initParams" />
    </div>

</template>

<script>
import { getList, del } from '@/api/${model.Name}'
import AddOrUpdate from './form/AddOrUpdate.vue'
import { getTypes } from '@/api/common'
export default {
    components: { AddOrUpdate },
    data() {
        return {
            showDetail: false,
            types: [],
            page: {
                pageNo: 1,
                pageSize: 10,
                total: 0
            },
            list: [],
            search: {

            },
            initParams: {

            }
        }
    },
    mounted() {
        this.init()
        this.fetchData()
    },
    methods: {
        init() {
            getTypes().then(res => {
                this.types = res.data
            })
        },
        fetchData() {
            getList({ pageNo: this.page.pageNo, pageSize: this.page.pageSize }).then(res => {
                this.list = res.data
                this.page.pageNo = res.pageNo
                this.page.pageSize = res.pageSize
                this.page.total = res.totalCount
            })
        },
        pageChange(page) {
            this.page.pageNo = page
            this.fetchData()
        },
        sizeChange(size) {
            this.page.pageSize = size
            this.pageChange(1)
        },
        handleEdit(row) {
            this.showDetail = true
            // 编辑用户信息
            this.initParams = {
                title: '修改',
                type: 1,
                id: row.id,
                cb: () => {
                    this.fetchData()
                }
            }
        },
        handleDelete(row) {
            // 删除用户信息
            this.${"$"}confirm('您确定要删除此条数据吗?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                // 发送请求删除用户信息
                del({id: row.id }).then(res => {
                    this.fetchData()
                    if (res.isSuccess) {
                        this.${"$"}message.success('操作成功')
                    } else {
                        this.${"$"}message.error(res.message)
                    }
                })
            }).catch(() => { })
        },
        handleSearch(value) {
            this.fetchData()
        },
        onAdd() {
            this.showDetail = true
            this.initParams = {
                title: '新增',
                type: 0,
                cb: () => {
                    this.fetchData()
                }
            }
        }
    }
}
</script>
