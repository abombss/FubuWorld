﻿using System.Web;
using FubuCore;
using FubuDocs.Topics;
using FubuMVC.CodeSnippets;
using FubuMVC.Core.UI;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuDocs.Navigation
{
    public static class TopicExtensions
    {
        public static HtmlTag BottleSnippetFor(this IFubuPage page, string snippetName)
        {
            var project = page.Get<ITopicContext>().Project;
            var snippets = page.Get<ISnippetCache>();

            if (project == null)
            {
                return page.CodeSnippet(snippetName);
            }

            // TODO -- get rid of the downcast here when the new SlickGrid bottle is ready
            var snippet = snippets.As<SnippetCache>().FindByBottle(snippetName, project.BottleName) ??
                          snippets.Find(snippetName);

            return page.CodeSnippet(snippet);
        }


        public static IHtmlString ProjectSummary(this IFubuPage page)
        {
            var project = page.Get<ITopicContext>().Project;
            return page.Partial(project);
        }

        public static HtmlTag TableOfContents(this IFubuPage page)
        {
            return page.Get<TopicTreeBuilder>().BuildTableOfContents();
        }

        public static TagList LeftTopicNavigation(this IFubuPage page)
        {
            return page.Get<TopicTreeBuilder>().BuildLeftTopicLinks().ToTagList();
        }

        public static TagList TopTopicNavigation(this IFubuPage page)
        {
            return page.Get<TopicTreeBuilder>().BuildTopTopicLinks().ToTagList();
        }

        public static HtmlTag LinkToTopic(this IFubuPage page, string name)
        {
            Topic topic = TopicGraph.AllTopics.Find(name);
            return new TopicLinkTag(topic);
        }
    }

    public class TopicLinkTag : HtmlTag
    {
        public TopicLinkTag(Topic topic) : base("a")
        {
            Attr("href", topic.AbsoluteUrl);
            Text(topic.Title);
            Attr("data-key", topic.Name);
        }
    }
}