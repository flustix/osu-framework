﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using osu.Framework.Input;
using OpenTK.Input;
using osu.Framework.Allocation;

namespace osu.Framework.Graphics.Containers
{
    /// <summary>
    /// An overlay container that eagerly holds keyboard focus.
    /// </summary>
    public abstract class FocusedOverlayContainer : OverlayContainer
    {
        private InputManager inputManager;

        public override bool RequestingFocus => State == Visibility.Visible;

        protected override bool OnFocus(InputState state) => true;

        protected override void OnFocusLost(InputState state)
        {
            if (state.Keyboard.Keys.Contains(Key.Escape))
                Hide();
            base.OnFocusLost(state);
        }

        [BackgroundDependencyLoader]
        private void load(UserInputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        protected override void PopIn()
        {
            Schedule(inputManager.TriggerFocusContention);
        }

        protected override void PopOut()
        {
            TriggerFocusLost();
        }
    }
}
