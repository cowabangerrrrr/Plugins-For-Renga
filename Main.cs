﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renga;

namespace PluginsForRenga
{
    public class PropertiesManagerPlugin: IPlugin
    {
        internal static Application m_app;
        private List<Renga.ActionEventSource> m_eventSources = new List<Renga.ActionEventSource>();

        public bool Initialize(string pluginFolder)
        {
            m_app = new Renga.Application();
            var ui = m_app.UI;
            var panelExtension = ui.CreateUIPanelExtension();

            panelExtension.AddToolButton(
              AddPropertiesAction(ui, "Добавить свойства"));
            panelExtension.AddToolButton(
              DeletePropertiesAction(ui, "Удалить свойства"));

            ui.AddExtensionToPrimaryPanel(panelExtension);
            ui.AddExtensionToActionsPanel(panelExtension, Renga.ViewType.ViewType_View3D);

            return true;
        }

        public void Stop()
        {
            foreach (var eventSource in m_eventSources)
                eventSource.Dispose();

            m_eventSources.Clear();
        }

        private Renga.IAction AddPropertiesAction(Renga.IUI ui, string displayName)
        {
            var action = ui.CreateAction();
            action.DisplayName = displayName;
            var events = new Renga.ActionEventSource(action);

            events.Triggered += (s, e) =>
            {
                var addingWindow = new AddingProperties();
                System.Windows.Forms.Application.Run(addingWindow);
                addingWindow.Close();
            };

            m_eventSources.Add(events);
            return action;
        }

        private Renga.IAction DeletePropertiesAction(Renga.IUI ui, string displayName)
        {
            var action = ui.CreateAction();
            action.DisplayName = displayName;
            var events = new Renga.ActionEventSource(action);


            events.Triggered += (s, e) =>
            {
                var deletingWindow = new DeletingProperties();
                System.Windows.Forms.Application.Run(deletingWindow);
                deletingWindow.Close();
            };

            m_eventSources.Add(events);
            return action;
        }
    }
}