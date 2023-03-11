using System;
using Engine.Animation.Base;
using Engine.Enum;

namespace Engine.Animation
{
    public class CharacterAnimationStateManager : BaseAnimationStateManager
    {
        private bool _statePlaysOnceOnChange;
        private bool _statePlaysOnLoop;
        private bool _stateAnimationIsPlayingOnce;
        private bool _stateAnimationIsLooping;
        private AnimationState _previousState = AnimationState.STATIC;

        public CharacterAnimationStateManager(AnimationState animationState) : base(animationState)
        {
            InitializeAnimationStateSettings();
        }

        private void InitializeAnimationStateSettings()
        {
            _statePlaysOnceOnChange = AnimationState == AnimationState.ROLLING ||
                AnimationState == AnimationState.JUMPING ||
                AnimationState == AnimationState.LANDING ? true : false;
            _statePlaysOnLoop = AnimationState == AnimationState.IDLE ||
               AnimationState == AnimationState.FALLING ? true : false;

            _stateAnimationIsPlayingOnce = _statePlaysOnceOnChange;
            _stateAnimationIsLooping = _statePlaysOnLoop;
        }

        public void UpdateAnimationState(AnimationState newState)
        {
            _previousState = AnimationState;
            AnimationState = newState;
            InitializeAnimationStateSettings();
        }

        public AnimationState PreviousState
        {
            get { return _previousState; }
        }

        public bool PlaysOnceOnChange
        {
            get { return _statePlaysOnceOnChange; }
        }

        public bool PlaysOnLoop
        {
            get { return _statePlaysOnLoop; }
        }

        public bool IsPlaying
        {
            get { return _stateAnimationIsPlayingOnce; }
        }

        public bool IsLooping
        {
            get { return _stateAnimationIsLooping; }
        }

        public void StartedPlaying()
        {
            _stateAnimationIsPlayingOnce = true;
        }

        public void StoppedPlaying()
        {
            _stateAnimationIsPlayingOnce = false;
        }

        public override double FrameDuration()
        {
            if (AnimationState == AnimationState.ROLLING) { return (18.0 / 60.0); }
            if (AnimationState == AnimationState.WALKING) { return (17.0 / 60.0); }
            if (AnimationState == AnimationState.RUNNING) { return (25.0 / 60.0); }
            if (AnimationState == AnimationState.IDLE) { return (10.0 / 60.0); }
            return (15.0 / 60.0);
        }
    }
}

