# 接口文档

[TOC]

## 用户

### 注册

POST 接收参数：account, nickName, pwd

### 登陆

POST 接收参数：account, pwd

## 文章

### 按条件分页查询文章
#### 请求URL

```
https://localhost:44333/api/article
```

#### 请求方式

```
GET
```

#### 参数表

| 参数      | 是否必选 | 类型 | 说明                                 |
| --------- | -------- | ---- | ------------------------------------ |
| pageSize  | yes      | int  | 页面容量                             |
| pageIndex | yes      | int  | 当前页码                             |
| isUseTime | no       | bool | 是否根据时间排序（默认根据热度排序） |

#### 返回示例

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

### 展示某用户的文章

#### 请求URL

```
https://localhost:44333/api/article
```

#### 请求方式

```
GET
```

#### 参数表

| 参数      | 是否必选 | 类型 | 说明                                 |
| --------- | -------- | ---- | ------------------------------------ |
| pageSize  | yes      | int  | 页面容量                             |
| pageIndex | yes      | int  | 当前页码                             |
| userId    | yes      | int  | 用户ID                               |
| isUseTime | no       | bool | 是否根据时间排序（默认根据热度排序） |

#### 返回示例

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

### 展示某篇文章

#### 请求URL
```
https://localhost:44333/api/article
```

#### 请求方式

```
GET
```

#### 参数表

| 参数      | 是否必选 | 类型 | 说明   |
| --------- | -------- | ---- | ------ |
| articleId | yes      | int  | 文章ID |

#### 返回示例

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



### 发布

POST 接收参数：title, content, authorId, guid

### 删除

POST 接收参数：articleId, guid