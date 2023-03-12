using System;
using System.Collections.Generic;
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
        private Dictionary<AnimationState, List<AnimationState>> _validNextStates = new Dictionary<AnimationState, List<AnimationState>>()
        {
            { AnimationState.JUMPING , new List<AnimationState>(){ AnimationState.FALLING }},
            { AnimationState.FALLING , new List<AnimationState>(){ AnimationState.LANDING }},
            { AnimationState.LANDING , new List<AnimationState>(){ AnimationState.IDLE,
                                                                   AnimationState.WALKING,
                                                                   AnimationState.RUNNING,}}
        };

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

        public bool NeedsToTransition
        {
            get {
                if(AnimationState ==  AnimationState.JUMPING && !_stateAnimationIsPlayingOnce)
                {
                    return true;
                }
                return false;

            }
        }

        public AnimationState TransitionToState(AnimationState desiredState)
        {
            if (_validNextStates.ContainsKey(AnimationState))
            {
                var validStates = _validNextStates[AnimationState];
                //Return for specific state
                foreach( var state in validStates)
                {
                    if(desiredState == state)
                    {
                        return desiredState;
                    }
                }

                //return if it can only go to the next available state
                if(validStates.Count > 0)
                {
                    return validStates[0];
                }

                

            }
            //if all else fails just return the state it wanted
            return desiredState;

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
            if (AnimationState == AnimationState.JUMPING) { return (18.0 / 60.0); }
            if (AnimationState == AnimationState.LANDING) { return (5.0 / 60.0); }
            return (15.0 / 60.0);
        }
    }
}

