namespace HA.PMS.ToolsLibrary
{
    public static class WebExtensions
    {
        #region System.Web.UI.Control

        /// <summary>
        /// 设置指定服务器控件的指定属性的值。
        /// </summary>
        /// <param name="control">服务器控件对象。</param>
        /// <param name="propertyName">服务器控件的属性名称。</param>
        /// <param name="value">设置的值。</param>
        /// <exception cref="System.Reflection.AmbiguousMatchException"></exception>
        public static void SetPropertyValue(this System.Web.UI.Control control, string propertyName, object value)
        {
            control.GetType().GetProperty(propertyName).SetValue(control, value);
        }

        /// <summary>
        /// 获取指定服务器控件的指定属性的值。
        /// </summary>
        /// <param name="control">服务器控件对象。</param>
        /// <param name="propertyName">服务器控件属性名称。</param>
        /// <exception cref="System.Reflection.AmbiguousMatchException"></exception>
        public static void GetPropertyValue(this System.Web.UI.Control control, string propertyName)
        {
            control.GetType().GetProperty(propertyName).GetValue(control);
        }

        #endregion

        #region System.Web.UI.WebControls.Repeater

        /// <summary>
        /// 将 System.Web.UI.WebControls.Repeater 控件及所有子控件绑定到指定的数据源。
        /// </summary>
        /// <param name="repeater">控件对象。</param>
        /// <param name="dataSource">提供数据的数据源。</param>
        public static void DataBind(this System.Web.UI.WebControls.Repeater repeater, object dataSource)
        {
            repeater.DataSource = dataSource;
            repeater.DataBind();
        }
        
        /// <summary>
        /// 将 System.Web.UI.WebControls.RepeaterItem 对象关联的数据项转换为指定的实体对象。
        /// </summary>
        /// <typeparam name="TSource">实体对象类型。</typeparam>
        /// <param name="e">System.Web.UI.WebControls.RepeaterCommandEventArgs 参数对象。</param>
        /// <returns></returns>
        public static TSource ToEntity<TSource>(this System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            return (TSource)e.Item.DataItem;
        }
        
        /// <summary>
        /// 将 System.Web.UI.WebControls.RepeaterItem 对象关联的数据项转换为指定的实体对象。
        /// </summary>
        /// <typeparam name="TSource">实体对象类型。</typeparam>
        /// <param name="e">System.Web.UI.WebControls.RepeaterItemEventArgs 参数对象。</param>
        /// <returns></returns>
        public static TSource ToEntity<TSource>(this System.Web.UI.WebControls.RepeaterItemEventArgs e) where TSource : class
        {
            return (TSource)e.Item.DataItem;
        }

        /// <summary>
        /// 将指定 ID 的服务器控件转换为指定类型服务器控件对象。
        /// </summary>
        /// <typeparam name="TSource">服务器控件类型。</typeparam>
        /// <param name="e">System.Web.UI.WebControls.RepeaterCommandEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        /// <returns></returns>
        public static TSource GetControl<TSource>(this System.Web.UI.WebControls.RepeaterCommandEventArgs e, string id) where TSource : System.Web.UI.Control
        {
            return (TSource)(object)e.Item.FindControl(id);
        }

        /// <summary>
        /// 将指定 ID 的服务器控件转换为指定类型服务器控件对象。
        /// </summary>
        /// <typeparam name="TSource">服务器控件类型。</typeparam>
        /// <param name="e">System.Web.UI.WebControls.RepeaterItemEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        /// <returns></returns>
        public static TSource GetControl<TSource>(this System.Web.UI.WebControls.RepeaterItemEventArgs e, string id) where TSource : System.Web.UI.Control
        {
            return (TSource)(object)e.Item.FindControl(id);
        }

        /// <summary>
        /// 设置指定服务器控件的 Text 或 Value 属性值。
        /// </summary>
        /// <param name="e">System.Web.UI.WebControls.RepeaterCommandEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        /// <param name="value">设置的值。</param>
        public static void SetTextValue(this System.Web.UI.WebControls.RepeaterCommandEventArgs e, string id, string value)
        {
            System.Web.UI.Control control = e.Item.FindControl(id);
            System.Reflection.PropertyInfo textPropertyInfo = control.GetType().GetProperty("Text");

            if (!object.ReferenceEquals(textPropertyInfo, null))
            {
                control.GetType().GetProperty("Text").SetValue(control, value);
            }
            else
            {
                control.GetType().GetProperty("Value").SetValue(control, value);
            }
        }

        /// <summary>
        /// 设置指定服务器控件的 Text 或 Value 属性值。
        /// </summary>
        /// <param name="e">System.Web.UI.WebControls.RepeaterItemEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        /// <param name="value">设置的值。</param>
        public static void SetTextValue(this System.Web.UI.WebControls.RepeaterItemEventArgs e, string id, string value)
        {
            System.Web.UI.Control control = e.Item.FindControl(id);
            System.Reflection.PropertyInfo textPropertyInfo = control.GetType().GetProperty("Text");

            if (!object.ReferenceEquals(textPropertyInfo, null))
            {
                control.GetType().GetProperty("Text").SetValue(control, value);
            }
            else
            {
                control.GetType().GetProperty("Value").SetValue(control, value);
            }
        }

        /// <summary>
        /// 获取指定服务端控件的 Text 或 Value 属性值。
        /// </summary>
        /// <param name="e">System.Web.UI.WebControls.RepeaterCommandEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        public static object GetTextValue(this System.Web.UI.WebControls.RepeaterCommandEventArgs e, string id)
        {
            System.Web.UI.Control control = e.Item.FindControl(id);
            System.Reflection.PropertyInfo textPropertyInfo = control.GetType().GetProperty("Text");

            if (!object.ReferenceEquals(textPropertyInfo, null))
            {
                return control.GetType().GetProperty("Text").GetValue(control);
            }
            else
            {
                return control.GetType().GetProperty("Value").GetValue(control);
            }
        }

        /// <summary>
        /// 获取指定服务端控件的 Text 或 Value 属性值。
        /// </summary>
        /// <param name="e">System.Web.UI.WebControls.RepeaterItemEventArgs 参数对象。</param>
        /// <param name="id">包含在容器 e 中的服务器控件 ID。</param>
        public static object GetTextValue(this System.Web.UI.WebControls.RepeaterItemEventArgs e, string id)
        {
            System.Web.UI.Control control = e.Item.FindControl(id);
            System.Reflection.PropertyInfo textPropertyInfo = control.GetType().GetProperty("Text");

            if (!object.ReferenceEquals(textPropertyInfo, null))
            {
                return control.GetType().GetProperty("Text").GetValue(control);
            }
            else
            {
                return control.GetType().GetProperty("Value").GetValue(control);
            }
        }

        #endregion

        #region System.Web.UI.WebControls.ListControl

        /// <summary>
        /// <para>用指定文本、值和启用数据初始化 System.Web.UI.WebControls.ListItem 并添加到 listControl。</para>
        /// <para>包括：CheckBoxList，DropDownList，ListBox，RadioButtonList，BulletedList。</para>
        /// </summary>
        /// <param name="listControl">继承自 System.Web.UI.WebControls.ListControl 的对象。</param>
        /// <param name="text">文本。</param>
        /// <param name="value">值。</param>
        /// <param name="enabled">是否启用。</param>
        public static void AddItem(this System.Web.UI.WebControls.ListControl listControl, string text, string value, bool enabled = true)
        {
            listControl.Items.Add(new System.Web.UI.WebControls.ListItem(text, value, enabled));
        }

        /// <summary>
        /// 将特性添加到服务器控件的 System.Web.UI.AttributeCollection 对象。
        /// </summary>
        /// <param name="control">继承自 System.Web.UI.WebControls.ListControl 的对象。</param>
        /// <param name="key">属性名。</param>
        /// <param name="value">属性值。</param>
        public static void AddAttribute(this System.Web.UI.WebControls.ListControl listControl, string key, string value)
        {
            listControl.Attributes.Add(key, value);
        }

        /// <summary>
        /// 从服务器控件的 System.Web.UI.AttributeCollection 对象中移除一个特性。
        /// </summary>
        /// <param name="control">继承自 System.Web.UI.WebControls.ListControl 的对象。</param>
        /// <param name="key">属性名。</param>
        public static void RemoveAttribute(this System.Web.UI.WebControls.ListControl listControl, string key)
        {
            listControl.Attributes.Remove(key);
        }

        #endregion
        
        #region System.Web.UI.WebControls.BaseDataList
        /// <summary>
        /// <para>将控件及其所有子控件绑定到指定的数据源。</para>
        /// <para>包括：DataGrid，DataList。</para>
        /// </summary>
        /// <param name="repeater">继承自 System.Web.UI.WebControls.BaseDataList 的控件对象。</param>
        /// <param name="dataSource">提供数据的数据源。</param>
        public static void DataBind(this System.Web.UI.WebControls.BaseDataList control, object dataSource)
        {
            control.DataSource = dataSource;
            control.DataBind();
        } 
        #endregion

        #region System.Web.UI.WebControls.BaseDataBoundControl
        /// <summary>
        /// <para>将指定数据源绑定到被调用的服务器控件及其所有子控件。</para>
        /// <para>包括：CheckBoxList，DropDownList，ListBox，ListView，，RadioButtonList。</para>
        /// </summary>
        /// <param name="control">继承自 System.Web.UI.WebControls.BaseDataBoundControl 的控件对象。</param>
        /// <param name="dataSource">提供数据的数据源。</param>
        public static void DataBind(this System.Web.UI.WebControls.BaseDataBoundControl control, object dataSource)
        {
            control.DataSource = dataSource;
            control.DataBind();
        } 
        #endregion
    }
}
