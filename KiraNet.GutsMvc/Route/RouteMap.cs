using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KiraNet.GutsMvc.Route
{
    public class RouteMap
    {
        public string Name { get; set; }
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
        public string DefaultParameter { get; set; }
        public string Template { get; set; }

        private readonly IList<string> _routeList = new List<string>(3);

        public IList<string> GetRouteList()
        {
            if(_routeList==null || _routeList.Count==0)
            {
                return null;
            }

            return _routeList;
        }

        public RouteMap(string name, string controller, string action, string parameter="id")
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (String.IsNullOrWhiteSpace(controller))
                throw new ArgumentNullException(nameof(controller));
            if (String.IsNullOrWhiteSpace(action))
                throw new ArgumentNullException(nameof(action));
            if (String.IsNullOrWhiteSpace(parameter))
                throw new ArgumentNullException(nameof(parameter));

            Name = name;
            DefaultController = controller;
            DefaultAction = action;
            DefaultParameter = parameter;
            Template = "{@}/{@}/{@}";
            _routeList.Add("controller");
            _routeList.Add("Action");
            _routeList.Add("parameter");
        }

        public RouteMap(string name = "default", string template = "{controller=home}/{action=index}/{id?}")
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                Name = String.Empty;
            }
            else
            {
                Name = name;
            }

            if (String.IsNullOrWhiteSpace(template))
            {
                throw new ArgumentNullException(nameof(template));
            }

            Template = template;
            this.SetMap(Template);
        }

        private void SetMap(string template)
        {
            // TODO： 此正则表达式还太过简单，不能应付所有情况，等以后再研究！！！
            var matchResults = Regex.Matches(template, @"({(\w+?.\w+?)}|{\w+?.})");
            if (matchResults.Count > 3)
            {
                throw new ArgumentException($"{nameof(template)}不符合规范");
            }

            for (int index = 0; index < matchResults.Count; index++)
            {
                if (!Valid(matchResults[index].Value))
                    throw new ArgumentOutOfRangeException(nameof(template));
            }

            Template = Regex.Replace(template, @"({(\w+?.\w+?)}|{\w+?.})", "{@}");
        }

        private bool Valid(string value)
        {
            var x = value.Trim('{', '}');
            if (x.StartsWith("controller", StringComparison.OrdinalIgnoreCase))
            {
                x = x.Substring(10).Trim();
                if (x != "" && x[0] == '=')
                {
                    x = x.Substring(1).ToLower();
                    foreach (var c in x)
                    {
                        if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != '_')
                            return false;
                    }

                    if (Char.IsLetter(x[0]) || x[0] == '_')
                    {
                        DefaultController = x;
                        _routeList.Add("controller");
                        return true;
                    }

                    return false;
                }
                else
                {
                    DefaultController = "home";
                    _routeList.Add("controller");
                    return true;
                }
            }

            if (x.StartsWith("action", StringComparison.OrdinalIgnoreCase))
            {
                x = x.Substring(6).Trim();
                if (x != "" && x[0] == '=')
                {
                    x = x.Substring(1).ToLower();
                    foreach (var c in x)
                    {
                        if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != '_')
                            return false;
                    }

                    if (Char.IsLetter(x[0]) || x[0] == '_')
                    {
                        DefaultAction = x;
                        _routeList.Add("action");
                        return true;
                    }

                    return false;
                }
                else
                {
                    DefaultAction = "index";
                    _routeList.Add("action");
                    return true;
                }
            }

            if (!Char.IsLetter(x[0]) && x[0] != '_')
                return false;

            if (x.EndsWith("?"))
            {
                x = x.TrimEnd('?');
                if (!String.IsNullOrWhiteSpace(x))
                {
                    DefaultParameter = x;
                    _routeList.Add("parameter");
                    return true;
                }

                DefaultParameter = "id";
                _routeList.Add("parameter");
                return true;
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(x))
                {
                    DefaultParameter = x;
                    _routeList.Add("parameter");
                    return true;
                }

                return false;
            }
        }
    }
}
