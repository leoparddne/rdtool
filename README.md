# RDTool

#### 介绍
研发过程中所需要的工具。
1. 支持多台windows服务器快速连接，提供服务器列表，双击即可连接服务器
2. virtualbox主机管理 - 需要部署RDTool-Server


#### 软件架构
软件架构说明


#### 安装教程

1.  使用环境为.net7

#### 使用说明
##### 客户端配置文件
示例:
```
{
  //服务器管理，暂时支持windows系统
  "Connections": [
    {
      "Name": "2.49",
      "IP": "192.168.2.49",
      "Port": 3389,
      "UserNname": "administrator"
    },
    {
      "Name": "2.48",
      "IP": "192.168.2.49",
      "Port": 3389,
      "UserNname": "administrator"
    },
    {
      "Name": "2.225",
      "IP": "192.168.2.225",
      "Port": 3389,
      "UserNname": "administrator"
    }
  ],
  
  //virtualbox主机管理使用
  "VMS": {
    //对应服务端地址
    "BaseURL": "http://192.168.2.40:9999"
  }
}
```

2. virtualbox虚拟机管理
需要将RDToolServer部署在virtualbox所在主机上,如需部署成系统服务,则需要配置服务运行时的身份为管理员。  
另外需要在服务端程序中配置virtualbox安装路径中的VBoxManage.exe所在位置


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
