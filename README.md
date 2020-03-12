# 接口文档

## 状态码

- `0`-成功
- `1`-服务器错误
- `2`-用户操作错误

## 权限等级

- `0`-普通用户
- `1`-管理员
- `99`-超级管理员

## 用户

### 注册

**请求URL:**

- `api/user/register`

**请求方式:**

- POST

**参数:**

| 参数     | 必选 | 类型   | 说明 |
| -------- | ---- | ------ | ---- |
| account  | yes  | string | 账号 |
| nickname | yes  | string | 昵称 |
| pwd      | yes  | string | 密码 |

**返回示例**

```json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 登陆 

**请求URL：** 
- `api/user/login `

**请求方式：**
- POST 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|account |yes  |string |账号   |
|pwd |yes  |string | 密码    |

 **返回示例**

``` json
 {
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "Guid": "95f1e723-eb3c-477d-9d0d-9d173d370fe4",
            "Account": "qwer",
            "Msg": null
        }
    ]
}
```

 **返回参数说明** 

|参数名|类型|说明|
|:-----  |:-----|-----                           |
|Guid |string   |Session验证ID  |

### 查看某人详情信息

**请求URL：** 

- `api/user/show/{userId}`

**请求方式：**
- GET

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userId |yes  |int |用户ID   |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "user": [
                {
                    "roleId": 5,
                    "nickName": "QQ",
                    "avatarUrl": null,
                    "account": "qwer",
                    "powerNum": 0
                }
            ],
            "likeList": [
                {
                    "articleId": 2,
                    "title": "测试文章1"
                }
            ]
        }
    ]
}
```

 **返回参数说明** 

|参数名|类型|说明|
|:-----  |:-----|-----                           |
|powerNum |int   |权限等级  |
|likeList |array |点赞列表 |

### 修改昵称/密码

**请求URL：** 
- `api/user/update/{userId} `

**请求方式：**
- POST 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userId |yes  |int |目标用户ID（URL传参）   |
|nickname |no  |string | 昵称    |
|pwd |no |string | 密码 |
|guid |yes |string | Session验证ID（登陆时获取） |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 管理员修改用户权限

**请求URL：** 
- `api/user/power/{userId} `

**请求方式：**
- POST 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userId |yes  |int |目标用户ID（URL传参）   |
|guid |yes  |string | 管理员Session验证ID（登陆时获取） |
|powernum |yes |int | 权限等级 |

 **返回示例**

``` json
 {
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 获取某用户权限等级

**请求URL：** 
- `api/user/getpower/{userId} `

**请求方式：**
- GET 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userId |yes  |int |目标用户ID   |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        99
    ]
}
```

 ### 上传头像

**简述：**

- `form-data`上传图片

**请求URL：** 
- `api/user/upload `

**请求方式：**
- POST 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|guid |yes  |string |Session验证ID（登陆时获取）   |
|image |yes  |file | 图片 |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        "avatars\\5f95f28f-7548-4ed3-9716-a5b59aefb3c4.png"
    ]
}
```

 **返回参数说明** 

|参数名|类型|说明|
|:-----  |:-----|-----                           |
|Data[0] |string   |服务器端图片路径  |


## 文章

### 按条件分页查询文章
**请求URL：**

- `api/article?pageSize=3&pageIndex=1&isUseTime=true`

**请求方式**

- GET

**参数:**

| 参数      | 是否必选 | 类型 | 说明                                 |
| --------- | -------- | ---- | ------------------------------------ |
| pageSize  | yes      | int  | 页面容量                             |
| pageIndex | yes      | int  | 当前页码                             |
| isUseTime | no       | bool | 是否根据时间排序（默认根据热度排序） |

**返回示例**

```js
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "articles": [
                {
                    "articleId": 8,
                    "title": "Hello World3",
                    "content": "<h1>Hello World3<h1><p>测试Session</p>",
                    "publishTime": "2020-03-07T17:49:09.667",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "昵称001"
                },
                {
                    "articleId": 7,
                    "title": "Hello World2",
                    "content": "<h1>Hello World2<h1>",
                    "publishTime": "2020-03-07T17:03:54.603",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "昵称001"
                }
            ],
            "totalCount": 3,
            "totalPages": 2
        }
    ]
}
```

**返回参数说明**

| 参数名     | 类型 | 说明   |
| ---------- | ---- | ------ |
| likeCount  | int  | 点赞数 |
| viewCount  | int  | 浏览量 |
| totalCount | int  | 总条数 |
| totalPages | int  | 总页数 |

### 展示某用户的文章

**请求URL:**

- `api/article?pageSize=2&pageIndex=1&isUseTime=true&userId=1`

**请求方式:**

- GET

**参数:**

| 参数      | 是否必选 | 类型 | 说明                                 |
| --------- | -------- | ---- | ------------------------------------ |
| pageSize  | yes      | int  | 页面容量                             |
| pageIndex | yes      | int  | 当前页码                             |
| userId    | yes      | int  | 用户ID                               |
| isUseTime | no       | bool | 是否根据时间排序（默认根据热度排序） |

**返回示例**

```js
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "articles": [
                {
                    "articleId": 8,
                    "title": "Hello World3",
                    "content": "<h1>Hello World3<h1><p>测试Session</p>",
                    "publishTime": "2020-03-07T17:49:09.667",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "昵称001"
                },
                {
                    "articleId": 7,
                    "title": "Hello World2",
                    "content": "<h1>Hello World2<h1>",
                    "publishTime": "2020-03-07T17:03:54.603",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "昵称001"
                }
            ],
            "totalCount": 3,
            "totalPages": 2
        }
    ]
}
```

**返回参数说明**

| 参数名     | 类型 | 说明   |
| ---------- | ---- | ------ |
| likeCount  | int  | 点赞数 |
| viewCount  | int  | 浏览量 |
| totalCount | int  | 总条数 |
| totalPages | int  | 总页数 |

### 展示某篇文章

**请求URL：**

- `api/article?articleId=1`

**请求方式：**

- GET

**参数：**

| 参数      | 是否必选 | 类型 | 说明   |
| --------- | -------- | ---- | ------ |
| articleId | yes      | int  | 文章ID |

**返回示例**

```js
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "article": {
                "articleId": 2,
                "title": "Hello World",
                "content": "<h1>Hello World</h1>",
                "likeCount": 0,
                "viewCount": 0,
                "publishTime": "1905-07-04T00:00:00",
                "nickName": "昵称001"
            },
            "commentIdList": []
        }
    ]
}
```

**返回参数说明**

### 发布文章

**请求URL：** 

- `api/article/publish`

**请求方式：**

- POST 

**参数：** 

| 参数名   | 必选 | 类型   | 说明          |
| :------- | :--- | :----- | ------------- |
| guid     | yes  | string | session验证id |
| title    | yes  | string | 标题          |
| content  | yes  | string | 内容          |
| authorId | yes  | int    | 作者id        |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 删除文章

**请求URL：** 

- `api/article/delete`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| articleId | yes  | int    | 文章ID        |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 修改文章

**请求URL：** 

- `api/article/update`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| articleId | yes  | int    | 文章ID        |
| title     | no   | string | 标题          |
| content   | no   | string | 内容          |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "title": "发表文章测试",
            "content": "<h1>发表文章测试</h1><p>这里是修改成功后的内容</p>"
        }
    ]
}
```

**返回数据说明**

- 均为修改后的数据

## 评论

### 根据文章ID获取评论

**请求URL：**

- `api/comment?pagesize=2&pageindex=1&articleId=1`

**请求方式**

- GET

**参数:**

| 参数      | 是否必选 | 类型 | 说明   |
| --------- | -------- | ---- | ------ |
| pagesize  | yes      | int  |        |
| pageindex | yes      | int  |        |
| articleId | yes      | int  | 文章ID |

**返回示例**

```js
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "comments": [
                {
                    "commentId": 4,
                    "publishTime": "2020-03-09T19:06:38.877",
                    "content": "Hello",
                    "articleId": 1,
                    "likeCount": 0,
                    "nickName": "我是用户3"
                },
                {
                    "commentId": 3,
                    "publishTime": "2020-03-09T19:06:04.64",
                    "content": "GKD",
                    "articleId": 1,
                    "likeCount": 0,
                    "nickName": "我是用户2"
                }
            ],
            "totalCount": 4,
            "totalPages": 2
        }
    ]
}
```

**返回参数说明**

| 参数名     | 类型 | 说明                   |
| ---------- | ---- | ---------------------- |
| likeCount  | int  | 点赞数(未设计点赞评论) |
| totalCount | int  | 总条数                 |
| totalPages | int  | 总页数                 |

### 添加评论

**请求URL：** 

- `api/comment/add`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| articleId | yes  | int    | 文章ID        |
| authorid  | yes  | int    | 评论作者id    |
| content   | yes  | string | 内容          |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

### 删除评论

**请求URL：** 

- `api/comment/delete`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| authorid  | yes  | int    | 评论作者id    |
| commentid | yes  | int    | 评论id        |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": null
}
```

## 点赞

### 点赞

**请求URL：** 

- `api/like/add`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| ArticleId | yes  | int    | 文章id        |
| userid    | yes  | int    | 点赞者id      |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        1
    ]
}
```

**返回参数说明**

| 参数名 | 类型 | 说明             |
| ------ | ---- | ---------------- |
| Data   | int  | 当前文章点赞总数 |

### 取消点赞

**请求URL：** 

- `api/like/delete`

**请求方式：**

- POST 

**参数：** 

| 参数名    | 必选 | 类型   | 说明          |
| :-------- | :--- | :----- | ------------- |
| guid      | yes  | string | session验证id |
| ArticleId | yes  | int    | 文章id        |
| userid    | yes  | int    | 取消点赞者id  |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        0
    ]
}
```

**返回参数说明**

| 参数名 | 类型 | 说明             |
| ------ | ---- | ---------------- |
| Data   | int  | 当前文章点赞总数 |

## 搜索

### 根据关键词搜索文章

**请求URL：** 

- `api/search?pagesize=3&pageindex=1&keyword=测试`

**请求方式：**

- GET

**参数：** 

| 参数名    | 必选 | 类型   | 说明     |
| :-------- | :--- | :----- | -------- |
| pagesize  | yes  | int    | 页面容量 |
| pageindex | yes  | int    | 当前页面 |
| keyword   | yes  | string | 关键词   |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "articles": [
                {
                    "articleId": 2,
                    "title": "测试文章1",
                    "content": "<h1>测试文章1</h1><p>测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试<p>",
                    "publishTime": "2020-03-09T18:35:41.263",
                    "likeCount": 1,
                    "viewCount": 0,
                    "nickName": "昵称001",
                    "roleId": 1
                },
                {
                    "articleId": 16,
                    "title": "发表文章测试",
                    "content": "<h1>发表要删除文章测试</h1><p>这里是准备删除的文章</p>",
                    "publishTime": "2020-03-11T16:14:30.033",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "QQ",
                    "roleId": 5
                },
                {
                    "articleId": 15,
                    "title": "发表文章测试",
                    "content": "<h1>发表文章测试</h1><p>这里是修改成功后的内容</p>",
                    "publishTime": "2020-03-11T16:07:44.853",
                    "likeCount": 0,
                    "viewCount": 0,
                    "nickName": "QQ",
                    "roleId": 5
                }
            ],
            "totalCount": 4,
            "totalPages": 2
        }
    ]
}
```

**返回参数说明**

| 参数名     | 类型 | 说明   |
| ---------- | ---- | ------ |
| totalCount | int  | 总条数 |
| totalPages | int  | 总页数 |

### 根据关键词搜索用户

**请求URL：** 

- `api/search/user?pagesize=3&pageindex=1&keyword=用户`

**请求方式：**

- GET

**参数：** 

| 参数名    | 必选 | 类型   | 说明     |
| :-------- | :--- | :----- | -------- |
| pagesize  | yes  | int    | 页面容量 |
| pageindex | yes  | int    | 当前页面 |
| keyword   | yes  | string | 关键词   |

 **返回示例**

``` json
{
    "Status": 0,
    "Msg": "SUCCESS",
    "Data": [
        {
            "users": [
                {
                    "roleId": 2,
                    "nickName": "我是用户1",
                    "avatarUrl": null
                },
                {
                    "roleId": 3,
                    "nickName": "我是用户2",
                    "avatarUrl": null
                },
                {
                    "roleId": 4,
                    "nickName": "我是用户3",
                    "avatarUrl": null
                }
            ],
            "totalCount": 3,
            "totalPages": 1
        }
    ]
}
```

**返回参数说明**

| 参数名     | 类型 | 说明   |
| ---------- | ---- | ------ |
| totalCount | int  | 总条数 |
| totalPages | int  | 总页数 |
