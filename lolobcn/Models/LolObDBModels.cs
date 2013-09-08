using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Web.Mvc;
using lolobcn.Depends;

namespace lolobcn.Models
{
    #region 上下文

    public partial class LolObDBModels : MySqlDBHelper
    {
        //private static MySqlDBHelper dbHelper = new MySqlDBHelper("name=LolObDBModelEntities");
        //private static MySqlDBHelper dbHelper = new MySqlDBHelper("server=localhost; port=3306; database=lolobcn_net; uid=root; pwd=gxh201100;",
        //                                                            MySqlDBHelper.ConnectionMode.BY_STRING);

        #region 构造函数

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModels 对象。
        /// </summary>
        public LolObDBModels() : base("name=LocalMySqlServices")
        {
            //
        }

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModels 对象。
        /// </summary>
        public LolObDBModels(string connStringName) : base(connStringName)
        {
            //
        }

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModels 对象。
        /// </summary>
        public LolObDBModels(string connString, ConnectionMode connMode) : base(connString, connMode)
        {
            //
        }

        #endregion
    }

    public partial class LolObDBModelsEx : MySqlDBHelperEx
    {
        //private static MySqlDBHelper dbHelper = new MySqlDBHelper("name=LolObDBModelEntities");
        //private static MySqlDBHelper dbHelper = new MySqlDBHelper("server=localhost; port=3306; database=lolobcn_net; uid=root; pwd=gxh201100;",
        //                                                            MySqlDBHelper.ConnectionMode.BY_STRING);

        #region 构造函数

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModelsEx 对象。
        /// </summary>
        public LolObDBModelsEx() : base("name=LocalMySqlServices")
        {
            //
        }

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModelsEx 对象。
        /// </summary>
        public LolObDBModelsEx(string connStringName) : base(connStringName)
        {
            //
        }

        /// <summary>
        /// 请使用应用程序配置文件的“LocalMySqlServices”部分中的连接字符串初始化新 LolObDBModelsEx 对象。
        /// </summary>
        public LolObDBModelsEx(string connString, ConnectionMode connMode) : base(connString, connMode)
        {
            //
        }

        #endregion
    }

    #endregion

    #region 实体

    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class MatchinfoTable : EntityObject
    {
        #region 数据库连接信息

        /// <summary>
        /// 在这里定义MatchinfoTable对象所对应的数据库查询sql语句
        /// </summary>
        public static string strSQL = "select id, serverId, serverName, serverIp, gameId from matchinfo";
        //public static string strSQL = "select * from matchinfo";

        #endregion

        #region 工厂方法

        /// <summary>
        /// 创建新的 MatchinfoTable 对象。
        /// </summary>
        /// <param name="id">Id 属性的初始值。</param>
        /// <param name="serverId">ServerId 属性的初始值。</param>
        /// <param name="serverName">ServerName 属性的初始值。</param>
        /// <param name="serverIp">ServerIp 属性的初始值。</param>
        /// <param name="gameId">GameId 属性的初始值。</param>
        public static MatchinfoTable CreateMatchinfoTable(global::System.UInt32 id, global::System.UInt32 serverId,
                                                        global::System.String serverName, global::System.String serverIp,
                                                        global::System.UInt32 gameId)
        {
            MatchinfoTable matchinfo = new MatchinfoTable();
            matchinfo.Id = id;
            matchinfo.ServerId = serverId;
            matchinfo.ServerName = serverName;
            matchinfo.ServerIp = serverIp;
            matchinfo.GameId = gameId;
            return matchinfo;
        }

        #endregion

        #region 基元属性

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public global::System.UInt32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    _Id = StructuralObject.SetValidValue(value);
                }
            }
        }
        private global::System.UInt32 _Id;

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public global::System.UInt32 ServerId
        {
            get
            {
                return _ServerId;
            }
            set
            {
                _ServerId = StructuralObject.SetValidValue(value);
            }
        }
        private global::System.UInt32 _ServerId;

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public global::System.String ServerName
        {
            get
            {
                return _ServerName;
            }
            set
            {
                _ServerName = StructuralObject.SetValidValue(value, false);
            }
        }
        private global::System.String _ServerName;

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public global::System.String ServerIp
        {
            get
            {
                return _ServerIp;
            }
            set
            {
                _ServerIp = StructuralObject.SetValidValue(value, false);
            }
        }
        private global::System.String _ServerIp;

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public global::System.UInt32 GameId
        {
            get
            {
                return _GameId;
            }
            set
            {
                _GameId = StructuralObject.SetValidValue(value);
            }
        }
        private global::System.UInt32 _GameId;

        #endregion
    }

    #endregion
}
