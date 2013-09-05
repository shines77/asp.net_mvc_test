﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace lolobcn.Models
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class LolObDBModelEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“LolObDBModelEntities”部分中的连接字符串初始化新 LolObDBModelEntities 对象。
        /// </summary>
        public LolObDBModelEntities() : base("name=LolObDBModelEntities", "LolObDBModelEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 LolObDBModelEntities 对象。
        /// </summary>
        public LolObDBModelEntities(string connectionString) : base(connectionString, "LolObDBModelEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 LolObDBModelEntities 对象。
        /// </summary>
        public LolObDBModelEntities(EntityConnection connection) : base(connection, "LolObDBModelEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Matchinfo> Matchinfo
        {
            get
            {
                if ((_Matchinfo == null))
                {
                    _Matchinfo = base.CreateObjectSet<Matchinfo>("Matchinfo");
                }
                return _Matchinfo;
            }
        }
        private ObjectSet<Matchinfo> _Matchinfo;

        #endregion
        #region AddTo 方法
    
        /// <summary>
        /// 用于向 Matchinfo EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToMatchinfo(Matchinfo matchinfo)
        {
            base.AddObject("Matchinfo", matchinfo);
        }

        #endregion
    }
    

    #endregion
    
    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="LolObDBModel", Name="Matchinfo")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Matchinfo : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Matchinfo 对象。
        /// </summary>
        /// <param name="id">Id 属性的初始值。</param>
        /// <param name="serverId">ServerId 属性的初始值。</param>
        /// <param name="serverName">ServerName 属性的初始值。</param>
        /// <param name="serverIp">ServerIp 属性的初始值。</param>
        /// <param name="gameId">GameId 属性的初始值。</param>
        public static Matchinfo CreateMatchinfo(global::System.Int32 id, global::System.Int32 serverId, global::System.String serverName, global::System.String serverIp, global::System.Int32 gameId)
        {
            Matchinfo matchinfo = new Matchinfo();
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
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ServerId
        {
            get
            {
                return _ServerId;
            }
            set
            {
                OnServerIdChanging(value);
                ReportPropertyChanging("ServerId");
                _ServerId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ServerId");
                OnServerIdChanged();
            }
        }
        private global::System.Int32 _ServerId;
        partial void OnServerIdChanging(global::System.Int32 value);
        partial void OnServerIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ServerName
        {
            get
            {
                return _ServerName;
            }
            set
            {
                OnServerNameChanging(value);
                ReportPropertyChanging("ServerName");
                _ServerName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ServerName");
                OnServerNameChanged();
            }
        }
        private global::System.String _ServerName;
        partial void OnServerNameChanging(global::System.String value);
        partial void OnServerNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ServerIp
        {
            get
            {
                return _ServerIp;
            }
            set
            {
                OnServerIpChanging(value);
                ReportPropertyChanging("ServerIp");
                _ServerIp = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ServerIp");
                OnServerIpChanged();
            }
        }
        private global::System.String _ServerIp;
        partial void OnServerIpChanging(global::System.String value);
        partial void OnServerIpChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 GameId
        {
            get
            {
                return _GameId;
            }
            set
            {
                OnGameIdChanging(value);
                ReportPropertyChanging("GameId");
                _GameId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("GameId");
                OnGameIdChanged();
            }
        }
        private global::System.Int32 _GameId;
        partial void OnGameIdChanging(global::System.Int32 value);
        partial void OnGameIdChanged();

        #endregion
    
    }

    #endregion
    
}
