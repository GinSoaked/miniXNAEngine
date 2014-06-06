using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheEngine
{
    public class Camera
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Vector3 viewportOffset;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 targetPosition;

        public Vector2 TargetPosition
        {
            get { return targetPosition; }
            set { targetPosition = value; }
        }


        private float zoom;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0)
                {
                    zoom = 0.0f;
                }
            }
        }
        private float targetZoom = 1;
        public float TargetZoom
        {
            get { return targetZoom; }
            set { targetZoom = value; }
        }

        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        private float targetRotation;
        public float TargetRotation
        {
            get { return targetRotation; }
            set { targetRotation = value; }
        }

        private int viewportWidth, viewportHeight;
        private Vector3 viewport;
        public Vector3 Viewport
        {
            get { return viewport; }
            set { viewport = value; }
        }

        private Vector3 zoomCentreOffset = Vector3.Zero;

        public Vector3 ZoomCentreOffset
        {
            get { return zoomCentreOffset; }
            set { zoomCentreOffset = value; }
        }

        private Vector3 zoomCentreTarget = Vector3.Zero;

        public Vector3 ZoomCentreTarget
        {
            get { return zoomCentreTarget; }
            set { zoomCentreTarget = value; }
        }

        public void SetZoomCentreTarget(float x, float y)
        {
            zoomCentreTarget.X = x;
            zoomCentreTarget.Y = y;
        }

        public void SetZoomCentreOffset(float x, float y)
        {
            zoomCentreOffset.X = x;
            zoomCentreOffset.Y = y;
        }


        public void SetViewport(int width, int height)
        {
            viewportWidth = width;
            viewportHeight = height;
            viewport.X = viewportWidth;
            viewport.Y = viewportHeight;
        }

        public void SetTargetPosition(float x, float y)
        {
            targetPosition.X = x;
            targetPosition.Y = y;
        }

        private Matrix transform;
        public Matrix Transform
        {
            get
            {
                transform =
                    
                    Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                    Matrix.CreateTranslation((Viewport * 0.5f)+zoomCentreOffset);
                return transform;
            }
        }

        private Matrix backwardsTransform;
        public Matrix BackwardsTransform
        {
            get
            {
                backwardsTransform =

                    Matrix.CreateTranslation(-(Viewport * 0.5f) + zoomCentreOffset) *
                    Matrix.CreateScale(new Vector3(1 / zoom, 1 / zoom, 1)) *
                    Matrix.CreateRotationZ(-rotation)*
                    Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0)) 
                    ;
                return backwardsTransform;
            }
        }

        public void Update(GameTime time)
        {
            Zoom = MathHelper.Lerp(Zoom, TargetZoom, 0.1f);

            Rotation = MathHelper.Lerp(Rotation, TargetRotation, 0.1f);

            zoomCentreOffset.X = MathHelper.Lerp(ZoomCentreOffset.X, ZoomCentreTarget.X, 0.1f);
            zoomCentreOffset.Y = MathHelper.Lerp(ZoomCentreOffset.Y, ZoomCentreTarget.Y, 0.1f);

            position.X = MathHelper.Lerp(Position.X, TargetPosition.X, 0.1f);
            position.Y = MathHelper.Lerp(Position.Y, TargetPosition.Y, 0.1f);
        }

        public Camera(string cameraName, Vector2 initialPosition)
        {
            name = cameraName;
            zoom = 1.0f;
            rotation = 0.0f;
            position = initialPosition;
        }

        public void ModifyPosition(Vector2 movement)
        {
            position += movement;
        }

        public void MoveTargetPosition(Vector2 movement)
        {
            targetPosition += movement;
        }

        public void RotateCamera(float rot)
        {
            targetRotation += rot;

            if (targetRotation == 360)
            {
                targetRotation = 0;
            }
        }

        public void ZoomCamera(float amount)
        {
            targetZoom += amount;
        }



    }
}
