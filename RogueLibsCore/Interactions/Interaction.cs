﻿using System;
using System.Collections.Generic;

namespace RogueLibsCore
{
    public abstract class Interaction : IComparable<Interaction>
    {
        public InteractionModel Model { get; internal set; } = null!; // initialized immediately in InteractionModel
        public PlayfieldObject Object => Model.Object;
        public Agent Agent => Model.Agent;
        public InteractionHelper Helper => Model.Helper;

        public virtual bool ImplicitAction => false;
        public int SortingOrder { get; set; }
        public int SortingIndex { get; set; }

        public virtual int CompareTo(Interaction? other)
        {
            if (other is null) return 1;
            int res = SortingOrder.CompareTo(other.SortingOrder);
            return res != 0 ? res : SortingIndex.CompareTo(other.SortingIndex);
        }

        public string? ButtonName { get; private set; }
        public int? ButtonPrice { get; private set; }
        public string? ButtonExtra { get; private set; }

        protected bool SetButton(string buttonName)
            => SetButton(buttonName, null, null);
        protected bool SetButton(string buttonName, string? buttonExtra)
            => SetButton(buttonName, null, buttonExtra);
        protected bool SetButton(string buttonName, int? buttonPrice)
            => SetButton(buttonName, buttonPrice, null);
        protected bool SetButton(string buttonName, int? buttonPrice, string? buttonExtra)
        {
            ButtonName = buttonName;
            ButtonPrice = buttonPrice;
            ButtonExtra = buttonExtra;
            return true;
        }

        protected void StopInteraction() => Model.StopInteraction();

        public abstract bool SetupButton();
        public abstract void OnPressed();
        public virtual void OnPressedImplicitly() => OnPressed();

        // TODO
        // public virtual void OnOpen() { }
        // public virtual void WhileOpen() { }
        // public virtual void OnClose() { }

        // public virtual void OnMouseEnter() { }
        // public virtual void OnMouseOver() { }
        // public virtual void OnMouseExit() { }

    }
    public interface IInteractionProvider
    {
        IEnumerable<Interaction> GetInteractions(InteractionModel model);
    }
}
