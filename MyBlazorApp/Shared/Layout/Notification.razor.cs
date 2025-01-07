using Microsoft.AspNetCore.Components;

namespace MyBlazorApp.Client.Shared.Layout;
public class NotificationComponentBase : ComponentBase
{
    protected bool _open;
    protected List<TwoStringItems> _content { get; set; }

    protected void ToggleOpen()
    {
        _open = !_open;

        VerifyNotificationContentList();
    }

    public void VerifyNotificationContentList()
    {
        _content ??=[];
        if (_content.Count > 1)
            _content = new();
    }


    public void AddMoreContent()
    {
        var newContent = new TwoStringItems(Guid.NewGuid(), "New Notification! 💻", "Scroll your browser to see effect.");
        _content.Add(newContent);
    }

    public void RemoveContent(Guid id)
    {
        var item = _content.FirstOrDefault(x => x.Id == id);
        _content.Remove(item);
    }
}
public record TwoStringItems(Guid Id, string Title, string Subtitle);
