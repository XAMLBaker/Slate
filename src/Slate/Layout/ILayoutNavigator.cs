using DryIoc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slate
{
    public interface ILayoutNavigator
    {
        Task NavigateToAsync(string url, object argu = null);
    }

    public class LayoutNavigator<T> : ILayoutNavigator
    {
        private readonly IContainer _container;

        public LayoutNavigator(IContainer container)
        {
            this._container = container;
        }

        public async Task NavigateToAsync(string url, object argu = null)
        {
            if (url[0] == '/' || url[0] == '.')
            {
                url = url.Remove (0, 1);
            }
            string _url = url.Replace ('/', '.');
            CreateLayout (_url, argu);
        }

        private void CreateLayout(string url, object argu)
        {
            try
            {
                bool _isGroupedWithLayout = IsGroupedWithLayout (url);
                bool _isGroupedWithContent = IsGroupedWithContent (url);
                string typeNameSpace = _isGroupedWithLayout ? GetLayoutString (url) : GetContentString (url);
                Type contentType = RegisterProvider.GetType (typeNameSpace);

                string moduleName = contentType.Assembly.GetName ().Name;
                int layoutCnt = (contentType.Namespace.Split ('.').Length - moduleName.Split ('.').Length);

                T rootLayout = default(T);
                for (int i = 0; i < layoutCnt; i++)
                {
                    var str = RemoveLastSegment (contentType.Namespace);
                    rootLayout = GetLayout (str);
                }

                if (rootLayout == null)
                    rootLayout = GetLayout (url, argu);
                else
                    GetLayout (url, argu);
            }
            catch (Exception ex)
            {

            }
        }

        private T GetLayout(string url, object argu = null)
        {
            if (RegisterProvider.HasPartialKeyMatch (url) == false)
                throw new Exception ("Module 등록되지 않은 url 입니다.");

            bool _isGroupedWithLayout = IsGroupedWithLayout (url);
            bool _isGroupedWithContent = IsGroupedWithContent (url);

            if ((_isGroupedWithLayout || _isGroupedWithContent) == false)
                return default (T);

            if (_isGroupedWithLayout)
            {
                Type layoutType = RegisterProvider.GetType (GetLayoutString (url));
                var layoutFrameworkElement = (T)this._container.Resolve (layoutType);
                ((IShellComponent)layoutFrameworkElement).RegionAttached (argu);

                if (_isGroupedWithContent == false)
                    return layoutFrameworkElement;
            }

            Type contentType = RegisterProvider.GetType (GetContentString (url));
            var contentFrameworkElement = (T)this._container.Resolve (contentType);
            ((IShellComponent)contentFrameworkElement).RegionAttached (argu);

            return contentFrameworkElement;
        }
        protected string GetLayoutString(string url)
        {
            if (url.Split ('.').Last () == "Layout")
                return url;

            return $"{url}.Layout";
        }

        protected string GetContentString(string url)
        {
            if (url.Split ('.').Last () == "Content")
                return url;

            return $"{url}.Content";
        }

        protected bool IsGroupedWithLayout(string url)
        {
            if (url.Split ('.').Last () == "Layout")
                return true;
            return RegisterProvider.IsUrlRegistered (GetLayoutString (url));
        }
        protected bool IsGroupedWithContent(string url)
        {
            if (url.Split ('.').Last () == "Content")
                return true;
            return RegisterProvider.IsUrlRegistered (GetContentString (url));
        }

        protected string RemoveLastSegment(string path)
        {
            int lastDot = path.LastIndexOf ('.');
            return lastDot >= 0 ? path.Substring (0, lastDot) : path;
        }
    }
}
