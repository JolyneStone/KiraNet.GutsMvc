using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Route
{
    // 根据RouteData和RouteMap 匹配路由
    public class RouteMatch : IRouteMatch
    {
        public bool Match(RouteMap map, string url, out RouteEntity routeEntity)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));
            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            routeEntity = new RouteEntity();
            var urlPath = url.TrimStart('/');
            var mapPath = map.Template.TrimStart('/');

            (bool result, IList<string> data) = Match(url, map);
            if (!result)
            {
                return false;
            }

            routeEntity = new RouteEntity();

            var _list = map.GetRouteList();
            for (int i = 0; i < data.Count && i < _list?.Count; i++)
            {
                switch (_list?[i].ToLower())
                {
                    case "controller": routeEntity.Controller = String.IsNullOrWhiteSpace(data[i]) ? map.DefaultController : data[i];
                            ; break;
                    case "action": routeEntity.Action = String.IsNullOrWhiteSpace(data[i]) ? map.DefaultAction : data[i];
                        break;
                    case "parameter":
                        routeEntity.DefaultParameter = map.DefaultParameter;
                        routeEntity.ParameterValue = data[i];
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证路由数据的存在的存在
        /// </summary>
        /// <param name="map"></param>
        /// <param name="url"></param>
        /// <returns>返回值依次是验证结果，路由的参数</returns>
        private (bool, IList<string>) Match(string url, RouteMap map)
        {
            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var template = map.Template;

            var a = url.IndexOf('?'); // 考虑到xxxx/1?xxx=xxxx的情况
            if(a>0)
            {
                url = url.Substring(0, a);
            }
            url.TrimStart('/');
            template.TrimStart('/');
            var s_template = template.Split('/');
            var s_url = url.Split('/');

            IList<string> list = new List<string>(3);

            for (int i = 0; i < s_url.Length && i < s_template.Length; i++)
            {
                var temp_index = s_template[i].IndexOf("{@}");
                var temp_url = s_url[i];
                var temp_template = s_template[i];
                while (temp_index >= 0)
                {
                    if (temp_index == 0)
                    {
                        if (temp_template.Length == 3)
                        {
                            list.Add(s_url[i]);
                            //temp_index = s_template[i].IndexOf("{@}", temp_index + 2);
                            break;
                        }
                        else if (temp_template.Length > 3)
                        {
                            // {@}的下一个字符必然是分隔符，我们就是这么定义的
                            var index = temp_url.IndexOf(temp_template[3]);
                            list.Add(temp_url.Substring(0, index));
                            temp_index = temp_template.IndexOf("{@}", 3);
                            temp_url = temp_url.Substring(index);
                            temp_template = temp_template.Substring(3);
                            continue;
                        }
                        else
                        {
                            // 不存在temp_template.Length < 3的情况
                            break;
                        }
                    }
                    else
                    {
                        var front = temp_template.Substring(0, temp_index);
                        if (temp_url.StartsWith(front, StringComparison.OrdinalIgnoreCase))
                        {
                            var index = temp_url.IndexOf(temp_template[temp_index + 3]);
                            list.Add(temp_url.Substring(temp_index, index));
                            temp_url = temp_url.Substring(index);
                            index = temp_index;
                            temp_index = temp_template.IndexOf("{@}", index + 3);
                            temp_template = temp_template.Substring(index + 3);
                        }
                    }
                }
            }

            return (list.Count != 0, list);
        }
    }
}
